using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmployeeAndPositionRepository: RepositoryBase<EmployeeAndPosition>, IEmployeeAndPositionRepository
{
    public EmployeeAndPositionRepository(AppDbContext context) : base(context)
    {
    }
}