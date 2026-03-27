using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmployeeAndPositionRepository: RepositoryBase<EmployeeAndPosition>, IEmployeeAndPositionRepository
{
    public EmployeeAndPositionRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<IEnumerable<EmployeeAndPosition>> FilterAsync(EmployeeAndPositionFilter? filter)
    {
        var query =  _dbSet.AsQueryable().AsNoTracking();
        
        if(filter.DatePosition.HasValue)
            query = query.Where(ep => ep.DatePosition >= filter.DatePosition.Value 
                                      && ep.DatePosition <= filter.DatePositionFinal.Value);
        
        if(filter.EmployeeId.HasValue)
            query = query.Where(ep => ep.EmployeeId == filter.EmployeeId);
        
        if(filter.PositionId.HasValue) 
            query = query.Where(ep => ep.PositionId == filter.PositionId);
        
        query = query.OrderBy(ep => ep.DatePosition);

        if (filter.Page.HasValue && filter.PageSize.HasValue)
        {
            int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
            query = query.Skip(skip).Take(filter.PageSize.Value);
        }

        return await query.ToListAsync();
    }
}