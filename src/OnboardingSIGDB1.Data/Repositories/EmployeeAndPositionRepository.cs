using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmployeeAndPositionRepository : RepositoryBase<EmployeeAndPosition>, IEmployeeAndPositionRepository
{
    public EmployeeAndPositionRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<(IEnumerable<EmployeeAndPosition> data, int total)> GetByFilterLinkAsync(EmployeeAndPositionFilter filter)
    {
       
        var query = _context.EmployeeAndPositions
            .Include(ep => ep.Employee)
            .Include(ep => ep.Position)
            .AsNoTracking();

        
        if (filter.EmployeeId.HasValue && filter.EmployeeId > 0)
        {
            query = query.Where(ep => ep.EmployeeId == filter.EmployeeId);
        }

        if (filter.PositionId.HasValue && filter.PositionId > 0)
        {
            query = query.Where(ep => ep.PositionId == filter.PositionId);
        }
        
        if (filter.DatePosition.HasValue && filter.DatePositionFinal.HasValue)
        {
            query = query.Where(ep => ep.DatePosition >= filter.DatePosition.Value && 
                                      ep.DatePosition <= filter.DatePositionFinal.Value);
        }
       
        else if (filter.DatePosition.HasValue)
        {
            query = query.Where(ep => ep.DatePosition.Date == filter.DatePosition.Value.Date);
        }
       
        var total = await query.CountAsync();

        
        int skip = (filter.Page - 1) * filter.PageSize;

        var data = await query
            .OrderByDescending(ep => ep.DatePosition) 
            .Skip(skip)
            .Take(filter.PageSize)
            .ToListAsync();

        return (data, total);
    }


    public async Task<EmployeeAndPosition?> LinkIsValid(int employeeid)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.EmployeeId == employeeid && x.Situation == true);
    }
    

    public async Task<EmployeeAndPosition> GetByDoubleId(int employeeId, int positionId)
    {
        var Link = await _dbSet
            .Include(x=>x.Position)
            .Include(x=> x.Employee)
            .FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.PositionId == positionId);
        return Link;
    }
}