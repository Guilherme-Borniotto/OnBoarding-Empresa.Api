using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface ICompanyRepository : IRepositoryBase<Company>
{

    Task<Company?> ExistsByCnpjAsync(string cnpj);
    Task<(IEnumerable<Company> data, int total)> GetByFilterCompanyAsync(CompanyFilter filter);
    Task<bool> ExistsEmployeeLinked(int id);
    Task<bool> ExistsPosition(int id);
    Task<bool> ExistsEmployee(int id);
}

