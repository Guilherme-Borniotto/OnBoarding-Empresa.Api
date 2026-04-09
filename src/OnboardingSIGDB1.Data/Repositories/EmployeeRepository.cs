using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Data.Repositories;

public class EmployeeRepository :RepositoryBase<Employee>,IEmployeeRepository
{
    public EmployeeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Employee?> ExistByCpfAsync(string CpfComparar)
    {
        return await _dbSet
            .FirstOrDefaultAsync(e => e.Cpf == CpfComparar);
    }
    
    public async Task<(IEnumerable<Employee> data, int total)> FilterAsync(EmployeeFilter filter)
    {
        var query = _dbSet.Include(e => e.Company).AsNoTracking(); // AsNoTracking melhora a performance de leitura

    
        if (!string.IsNullOrWhiteSpace(filter.Cpf))
            filter.Cpf = StringUtils.removemask(filter.Cpf);

     
        if (!string.IsNullOrWhiteSpace(filter.companyName))
            query = query.Where(e => e.Company.Name.Contains(filter.companyName));
    
        if (!string.IsNullOrWhiteSpace(filter.Name))
            query = query.Where(e => e.Name.Contains(filter.Name));

        if (!string.IsNullOrWhiteSpace(filter.Cpf))
            query = query.Where(e => e.Cpf == filter.Cpf);

   
        if (filter.HiredFrom.HasValue)
            query = query.Where(e => e.HireDate >= filter.HiredFrom.Value.Date);

        if (filter.HiredTo.HasValue)
        {
            // Pega até o último instante do dia informado
            var finalDoDia = filter.HiredTo.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(e => e.HireDate <= finalDoDia);
        }

        var total = await query.CountAsync();

        // 4. Paginação
        var data = await query
            .OrderBy(c => c.Name)
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (data, total);
    }

    public override async Task<Employee?> GetByIdAsync(int id)
    {
        return await _dbSet
            .Include(e => e.Company) 
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}