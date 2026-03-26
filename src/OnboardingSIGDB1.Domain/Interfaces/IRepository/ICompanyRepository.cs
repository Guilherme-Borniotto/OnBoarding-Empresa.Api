using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface ICompanyRepository : IRepositoryBase<Company>
{

    Task<bool> ExistsByCnpjAsync(string cnpj);
    Task <IEnumerable<Company>> FiltersAync(CompanyFilter filter);

}