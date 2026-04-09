using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Domain.Interfaces.IServices;

public interface IEmployeeService
{
    Task <ReadEmployeeDto>UpdateAsync(UpdateEmployeeDto dto, int id);
    Task DeleteAsync(int id);
    Task<ReadEmployeeDto> GetByIdAsync(int id);
    Task<ReadEmployeeDto> AddAsync(CreateEmployeeDto dto);
    Task<PagedResponse<ReadEmployeeDto>> GetByFiltersAsync(EmployeeFilter filter);
}