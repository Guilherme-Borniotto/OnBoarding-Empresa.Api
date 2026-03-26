using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmployeeRepository :RepositoryBase<Employee>,IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistByCpfAsync(string CpfComparar)
    {
        return await _dbSet
            .AnyAsync(e => e.Cpf == CpfComparar);
    }
    
    public async Task<IEnumerable<Employee>> FilterAsync(EmployeeFilter filter)
    {
        var query = _dbSet.AsQueryable();
// filter name = nome filtrado // busca por caracter unitario
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(e => e.Name.Contains(filter.Name));

        
        // so busca se digitar cmompleto
        if (!string.IsNullOrWhiteSpace(filter.Cpf))
            query = query.Where(e => e.Cpf == filter.Cpf);

        if (filter.HiredFrom.HasValue)
            query = query.Where(e => e.HireDate >= filter.HiredFrom.Value);

        if (filter.HiredTo.HasValue)
            query = query.Where(e => e.HireDate <= filter.HiredTo.Value);

        
        // PAGINAÇÃO
        if (filter.Page.HasValue && filter.PageSize.HasValue)
        {
            int skip = (filter.Page.Value - 1) * filter.PageSize.Value;
            query = query.Skip(skip).Take(filter.PageSize.Value);
        }

        return await query.OrderBy(e => e.Name).ToListAsync();
    }
    
}