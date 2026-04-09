using AutoMapper;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Responses;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUnitOfWork _uow;
    private readonly INotificationContext _notification;

    public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository, IUnitOfWork uow, ICompanyRepository companyRepository, INotificationContext notification)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
        _companyRepository = companyRepository;
        _uow = uow;
        _notification = notification;
    }

    public async Task<ReadEmployeeDto> AddAsync(CreateEmployeeDto dto)
    {
       
        var companyexist = await _companyRepository.GetByIdAsync(dto.CompanyId);
        
        if (companyexist is null)
        {
            _notification.AddNotification("CompanyId", "there is no company with this id");
            return null;
        }
        
        var result = await _employeeRepository.ExistByCpfAsync(dto.Cpf);
        
        if (result is not null && result.CompanyId != dto.CompanyId )
        {
            _notification.AddNotification("CPF", "CPF already exists.");
            return null;
        }

        if(dto.HireDate.HasValue && companyexist.FoundationDate.HasValue)
        {
            if (dto.HireDate < companyexist.FoundationDate)
            {
                _notification.AddNotification("HireDate", "date must be later than the company's founding date.");
            }
        }
        
        var employee = new Employee(dto.Name, dto.Cpf, dto.HireDate, dto.CompanyId);
        
        
        if (!employee.Validate())
        {
            _notification.DomainAddNotification(employee.ValidationResult.Errors);
            return null;
        }

        await _employeeRepository.AddAsync(employee);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "error while persisting the data.");
        }
        
        var employeecompleted = await _employeeRepository.GetByIdAsync(employee.Id);
        return _mapper.Map<ReadEmployeeDto>(employeecompleted);
        
    }

      public async Task<ReadEmployeeDto> UpdateAsync(UpdateEmployeeDto dto, int id)
    {
        var employeeExists = await _employeeRepository.GetByIdAsync(id);
        
        if (employeeExists == null)
        {
            _notification.AddNotification("EmployeeId", "Employee not found.");
            return null;
        }

       var result = await _employeeRepository.ExistByCpfAsync(dto.Cpf);
       if (result != null && result.Id != id)
       {
           _notification.AddNotification("Cpf", "This Cpf is already in use by another Employee.");
           return null;
       }

        if (dto.HireDate > DateTime.Now)
        {
            _notification.AddNotification("HireDate", "The date is in the future.");
            return null;  
        }
        
        
        _mapper.Map(dto, employeeExists);

        if (!employeeExists.Validate())
        {
            _notification.DomainAddNotification(employeeExists.ValidationResult.Errors);
            return null;
        }

        _employeeRepository.Update(employeeExists);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "Error updating data.");
            return null;
        }
        
        var employee = await _employeeRepository.GetByIdAsync(id);
        return  _mapper.Map<ReadEmployeeDto>(employee);
    }

    public async Task DeleteAsync(int id)
    {
        var EmployeeExists = await _employeeRepository.GetByIdAsync(id);
        if (EmployeeExists == null)
        {
            _notification.AddNotification("EmployeeId", "Employee not found.");
            return;
        }

        _employeeRepository.Delete(EmployeeExists);
        if (!await _uow.Commit())
        { 
            _notification.AddNotification("Database", "Error deleting the Employee.");
            
        }
    }

    public async Task<ReadEmployeeDto> GetByIdAsync(int id)
    {
        var EmployeeExists = await _employeeRepository.GetByIdAsync(id);
        return _mapper.Map<ReadEmployeeDto>(EmployeeExists);
    }


    public async Task<PagedResponse<ReadEmployeeDto>> GetByFiltersAsync(EmployeeFilter filter)
    {
        var (employees, total) = await _employeeRepository.FilterAsync(filter);
        var employeesDto = _mapper.Map<IEnumerable<ReadEmployeeDto>>(employees);
        
        return new PagedResponse<ReadEmployeeDto>(
            employeesDto, 
            filter.Page, 
            filter.PageSize, 
            total
        );
    }
}