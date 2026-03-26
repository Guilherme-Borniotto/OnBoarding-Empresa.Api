using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface IEmployeeRepository : IRepositoryBase<Employee>
{
    
    Task<bool> ExistByCpfAsync(string cpf);
    Task<IEnumerable<Employee>> FilterAsync(EmployeeFilter filter);
    
}