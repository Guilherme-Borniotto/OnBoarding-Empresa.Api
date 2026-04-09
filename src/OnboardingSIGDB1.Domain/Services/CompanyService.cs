using AutoMapper;
using Microsoft.VisualBasic;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Responses;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class CompanyService : ICompanyService

{
    private readonly ICompanyRepository _companyRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPositionRepository _positionRepository;
    private readonly IEmployeeAndPositionRepository _employeeAndPositionRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly INotificationContext _notification;

    public CompanyService(ICompanyRepository companyRepository,
                          IPositionRepository positionRepository,
                          IEmployeeAndPositionRepository employeeAndPositionRepository,  
                          IUnitOfWork uow, 
                          IMapper mapper, 
                          INotificationContext notification,
                          IEmployeeRepository  employeeRepository)
    {
        _companyRepository = companyRepository;
        _positionRepository = positionRepository;
        _uow = uow;
        _mapper = mapper;
        _notification = notification;
        _employeeRepository = employeeRepository;
        _employeeAndPositionRepository = employeeAndPositionRepository;
    }

    public async Task<ReadCompanyDto?> AddAsync(CreateCompanyDto dto)
    {
      
        if ( _companyRepository.ExistsByCnpjAsync(dto.Cnpj).Result != null)
        {
            _notification.AddNotification("CNPJ", "Company already registered with this CNPJ.");
            return null ;
        }
        
        var company = new Company(dto.Name, dto.Cnpj,dto.FoundationDate);

        
        if (!company.Validate())
        {
            _notification.DomainAddNotification(company.ValidationResult.Errors);
            return null;
        }
        
        await _companyRepository.AddAsync(company);

        
        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "error while persisting the data.");
        }

        var read = _mapper.Map<ReadCompanyDto>(company);

        return read;
    }

    public async Task<ReadCompanyDto?> UpdateAsync(UpdateCompanyDto dto, int id)
    {
        
        var companyExists = await _companyRepository.GetByIdAsync(id);
        if (companyExists == null)
        {
            _notification.AddNotification("CompanyId", "Company not found.");
            return null;
        }
        
       
        var companyWithCnpj = await _companyRepository.ExistsByCnpjAsync(dto.Cnpj);
        if (companyWithCnpj != null && companyWithCnpj.Id != id)
        {
            _notification.AddNotification("CNPJ", "This CNPJ is already in use by another company.");
            return null;
        }

      
        _mapper.Map(dto, companyExists);

        if (!companyExists.Validate())
        {
            _notification.DomainAddNotification(companyExists.ValidationResult.Errors);
            return null;
        }

        _companyRepository.Update(companyExists);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "Error while saving changes.");
            return null;
        }

        var company = await _companyRepository.GetByIdAsync(companyExists.Id);
        return _mapper.Map<ReadCompanyDto>(company);
    }
    public async Task Delete(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
        {
            _notification.AddNotification("CompanyId", "Company not found for deletion.");
            return;
        }

        if (_companyRepository.ExistsEmployeeLinked(id).Result)
        {
         _notification.AddNotification("Company","Exist employees linked this company");
         return;
        }

        _companyRepository.Delete(company);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("CompanyId", "Error deleting the company.");
        }
    }


    public async Task<ReadCompanyDto?> GetByIdAsync(int id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
            

       if (company == null)
       {
           _notification.AddNotification("Company","Company has no values");
           return null;
       }
       var read =_mapper.Map<ReadCompanyDto>(company);

       return read;
    }

    public async Task<PagedResponse<ReadCompanyDto>> GetByFiltersAsync(CompanyFilter filter)
    {
        var (data, total) = await _companyRepository.GetByFilterCompanyAsync(filter);
        var companyDtos = _mapper.Map<IEnumerable<ReadCompanyDto>>(data);

         
        return new PagedResponse<ReadCompanyDto>(
            companyDtos, 
            (int)filter.Page, 
            (int)filter.PageSize, 
            total
        );
    }

    public async Task<ReadEmployeeAndPositionDto?> AddPositionLink(CreateEmployeeAndPositionDto dto)
    {
        
        var employeeIsValid = await _employeeRepository.GetByIdAsync(dto.EmployeeId);
        if (employeeIsValid is null)
        {
            _notification.AddNotification("EmployeeId", "the employee id is invalid");
            return null;
        }
        
        var positionisValid = await _positionRepository.GetByIdAsync(dto.PositionId);
        if (positionisValid is null)
        {
            _notification.AddNotification("positionId", "the Position id is invalid");
            return null;
        }
        
        var existingLink = await _employeeAndPositionRepository.GetByDoubleId(dto.EmployeeId, dto.PositionId);
        if (existingLink != null)
        {
            _notification.AddNotification("Link", "This employee already holds (or has held) this position. Use Update to reactivate.");
            return null;
        }

        if (dto.DatePosition.HasValue)
        {
            if (dto.DatePosition < employeeIsValid.HireDate)
            {
                _notification.AddNotification("DatePosition", "the start date of the employment relationship must not be earlier than the admission date");
            }
        }
        
        var currentActive = await _employeeAndPositionRepository.LinkIsValid(dto.EmployeeId);
        if (currentActive != null)
        {
            currentActive.changeSituationForFalse();
            _employeeAndPositionRepository.Update(currentActive);
        }
        
        var newLink = _mapper.Map<EmployeeAndPosition>(dto);
        newLink.changeSituationForTrue(); 

        if (!newLink.Validate())
        {
            _notification.DomainAddNotification(newLink.ValidationResult.Errors);
            return null;
        }

        await _employeeAndPositionRepository.AddAsync(newLink);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Link", "Error while persisting.");
            return null;
        }

        var result = await _employeeAndPositionRepository.GetByDoubleId(newLink.EmployeeId, newLink.PositionId);
        
        return _mapper.Map<ReadEmployeeAndPositionDto>(result);
    }
    
    public async Task<PagedResponse<ReadEmployeeAndPositionDto>> GetByFilterLinkAsync(EmployeeAndPositionFilter filter)
    {
        var (links, total) = await _employeeAndPositionRepository.GetByFilterLinkAsync(filter);
        
        var dtos = _mapper.Map<IEnumerable<ReadEmployeeAndPositionDto>>(links);

        return new PagedResponse<ReadEmployeeAndPositionDto>(dtos, filter.Page, filter.PageSize, total);
    }
    
    public async Task<ReadEmployeeAndPositionDto> GetByLinkIdAsync(int employeeid, int positionid)
    {
        var links = await _employeeAndPositionRepository.GetByDoubleId(employeeid,positionid);
        
        if(links is null)
            _notification.AddNotification("LinkIds","We did not find a connection with this employee and position");
            
        return _mapper.Map<ReadEmployeeAndPositionDto>(links);
    }
    
    public async Task<ReadEmployeeAndPositionDto?> UpdatePositionLinkAsync(UpdateEmployeeAndPositionDto dto, int employeeid, int positiondId)
    {
        var link = await _employeeAndPositionRepository.GetByDoubleId(employeeid, positiondId);
    
        if (link == null)
        {
            _notification.AddNotification("Link", "Bond not found for this employee and position.");
            return null;
        }
        
        if (dto.Situation)
        {
            var currentActive = await _employeeAndPositionRepository.LinkIsValid(employeeid);
            if (currentActive != null && currentActive.PositionId != positiondId)
            {
                currentActive.changeSituationForFalse();
                _employeeAndPositionRepository.Update(currentActive);
            }
            
            link.changeSituationForTrue();
        }
        else 
        {
            link.changeSituationForFalse();
        }
        
        _mapper.Map(dto, link);
        
        if (!link.Validate())
        {
            _notification.DomainAddNotification(link.ValidationResult.Errors);
            return null;
        }

        _employeeAndPositionRepository.Update(link);
        
        if (!await _uow.Commit())
        {
            _notification.AddNotification("Link", "Erro ao persistir as alterações no banco.");
            return null;
        }
        
        return _mapper.Map<ReadEmployeeAndPositionDto>(link);
    }
}