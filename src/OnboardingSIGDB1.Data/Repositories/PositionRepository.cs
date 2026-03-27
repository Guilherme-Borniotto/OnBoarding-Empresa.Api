using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class PositionRepository : RepositoryBase<Position>,IPositionRepository
{
    public PositionRepository(AppDbContext context) : base(context)
    {
    }
    
    //fiter é apenas visualização portanto query pode ser as no tracking permitindo melhor gerenciamento da memoria, ef n gasta memoria para monitorar o objeto filter
    public async Task<IEnumerable<Position>> FilterAsync(PositionFilter filter)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(e => e.Name.Contains(filter.Name));
        
       
        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(e => e.Description.Contains(filter.Description));
        

        
        // PAGINAÇÃO
        if (filter.Page.HasValue && filter.PageSize.HasValue)
        {
            int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
            query = query.Skip(skip).Take(filter.PageSize.Value);
        }

        return await query.OrderBy(e => e.Name).ToListAsync();
    }
    
}
