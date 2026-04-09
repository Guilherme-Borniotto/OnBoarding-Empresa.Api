using System.Runtime.InteropServices.JavaScript;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Data.Repositories;

public class CompanyRepository : RepositoryBase<Company>,ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsEmployeeLinked(int id)
    {
       return await _context.Employees.AnyAsync(e => e.CompanyId == id);
    }

    public async Task<bool> ExistsPosition(int id)
    {
        return await _context.Positions.AnyAsync(p => p.Id == id);
    }
    public async Task<bool> ExistsEmployee(int id)
    {
        return await _context.Employees.AnyAsync(p => p.Id == id);
    }
    

    public async Task<Company?> ExistsByCnpjAsync(string cnpj)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Cnpj == cnpj);
    }

    public async Task<(IEnumerable<Company> data, int total)> GetByFilterCompanyAsync(CompanyFilter filter)
    {
        var query = _dbSet
            .AsNoTracking();
        
        if (!string.IsNullOrWhiteSpace(filter.Cnpj))
            filter.Cnpj = StringUtils.removemask(filter.Cnpj);
        
        if (!string.IsNullOrWhiteSpace(filter.Name)) 
            query = query.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));

        if (!string.IsNullOrWhiteSpace(filter. Cnpj))
            query = query.Where(c => c.Cnpj == filter.Cnpj);

      
        if (filter.Foundation.HasValue && filter.Deadline.HasValue)
        {
            query = query.Where(c => c.FoundationDate >= filter.Foundation.Value && 
                                     c.FoundationDate <= filter.Deadline.Value);
        }
        else if (filter.Foundation.HasValue) 
        {
            query = query.Where(c => c.FoundationDate == filter.Foundation.Value);
        }
        else if(filter.Deadline.HasValue)
        {
            query = query.Where(c => c.FoundationDate <= filter.Deadline.Value);
        }
    
        
        var total = await query.CountAsync();

       
        int? skip = (filter.Page - 1) * filter.PageSize;

        var data = await query
            .OrderBy(c => c.Name)
            .Skip((int)skip)
            .Take((int)filter.PageSize)
            .ToListAsync();

        return (data, total);
    }
    
    
    
}