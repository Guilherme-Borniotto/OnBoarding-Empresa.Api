using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Data.Repositories;

public class PositionRepository : RepositoryBase<Position>,IPositionRepository
{
    public PositionRepository(AppDbContext context) : base(context)
    {
    }
    

    public async Task<(IEnumerable<Position> data, int total)> FilterAsync(PositionFilter filter)
    {
        var query = _dbSet.AsNoTracking();

        if (!string.IsNullOrEmpty(filter.Name))
            query = query.Where(e => e.Name.Contains(filter.Name));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(e => e.Description.Contains(filter.Description));

        var total = await query.CountAsync();

        var data = await query
            .OrderBy(e => e.Name)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (data, total);
    }

    public async Task<bool> AnyByPositionIdAsync(int positionId)
    {
     
        return await _context.EmployeeAndPositions
            .AnyAsync(ep => ep.PositionId == positionId);
    }

    public async Task<bool> ExistsSameName(string name, string description)
    {
        return await _dbSet.AnyAsync(e => e.Name == name && e.Description == description);
    }
  
}
