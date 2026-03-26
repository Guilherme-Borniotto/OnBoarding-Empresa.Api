using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface IEmployeeAndPositionRepository : IRepositoryBase<EmployeeAndPosition>
{
    Task<IEnumerable<EmployeeAndPosition>> FilterAsync(EmployeeAndPositionFilter filter);
}