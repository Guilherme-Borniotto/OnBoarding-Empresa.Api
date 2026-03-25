using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories.Base;
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
}