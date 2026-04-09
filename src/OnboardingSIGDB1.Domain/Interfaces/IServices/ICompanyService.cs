using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Domain.Interfaces.IServices;

public interface ICompanyService
{
    Task <ReadCompanyDto?>AddAsync(CreateCompanyDto dto);
    Task<PagedResponse<ReadCompanyDto>> GetByFiltersAsync(CompanyFilter filter);
    Task<ReadCompanyDto?> GetByIdAsync(int id);
    Task Delete(int id);
    Task<ReadCompanyDto?> UpdateAsync(UpdateCompanyDto company, int id);
    
    //--------------------------------------------------------

    Task<ReadEmployeeAndPositionDto?> AddPositionLink(CreateEmployeeAndPositionDto dto);
    Task<PagedResponse<ReadEmployeeAndPositionDto>> GetByFilterLinkAsync(EmployeeAndPositionFilter filter);
    Task<ReadEmployeeAndPositionDto> GetByLinkIdAsync(int employeeid, int positionid);
    Task<ReadEmployeeAndPositionDto?> UpdatePositionLinkAsync(UpdateEmployeeAndPositionDto dto, int employeeid, int positiondId);
    

}