using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class CompanyRepository : RepositoryBase<Company>,ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }


    public async Task<bool> ExistsByCnpjAsync(string cnpj)
    {
       return await _dbSet.AnyAsync(e => e.Cnpj == cnpj);
    }

    public async Task<IEnumerable<Company>> FiltersAync(CompanyFilter filter)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();
        
       if(!string.IsNullOrWhiteSpace(filter.Name)) 
           query = query.Where(c=> c.Name.Contains(filter.Name));
        
       if(!string.IsNullOrWhiteSpace(filter.Cnpj))
           query = query.Where(c=>c.Cnpj == filter.Cnpj);
       
       if(filter.Foundation.HasValue) 
           query = query.Where(c=> c.FoundationDate == filter.Foundation);

       if (filter.CreatedAtCompany.HasValue)
           query = query.Where(c=> c.CreatedAt == filter.CreatedAtCompany.Value);
       
       if(  filter.CreatedAtCompany.HasValue && filter.Deadline.HasValue )
           query = query.Where( c => c.CreatedAt >= filter.CreatedAtCompany.Value && c.CreatedAt <= filter.Deadline.Value );

       // PAGINAÇÃO
       if (filter.Page.HasValue && filter.PageSize.HasValue)
       {
           int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
           query = query.Skip(skip).Take(filter.PageSize.Value);
       }
  
       return await query.OrderBy(e => e.Name).ToListAsync();
    }
    
}