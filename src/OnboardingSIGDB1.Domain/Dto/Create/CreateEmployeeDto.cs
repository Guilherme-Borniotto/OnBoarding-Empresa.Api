using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Create;

public class CreateEmployeeDto
{
    public string Name { get;  set; }
    public string Cpf { get;  set; }
    [DataType(DataType.Date)]
    public DateTime? HireDate { get;set; }
    public int CompanyId {get;  set;}
}