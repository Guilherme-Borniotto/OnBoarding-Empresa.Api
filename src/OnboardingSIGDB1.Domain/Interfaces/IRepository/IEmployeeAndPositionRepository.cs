using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface IEmployeeAndPositionRepository : IRepositoryBase<EmployeeAndPosition>
{
    Task<(IEnumerable<EmployeeAndPosition> data, int total)> GetByFilterLinkAsync(EmployeeAndPositionFilter filter);
    Task<EmployeeAndPosition?> LinkIsValid(int employeeid);
    Task<EmployeeAndPosition?> GetByDoubleId(int employeeid, int positonId);
}