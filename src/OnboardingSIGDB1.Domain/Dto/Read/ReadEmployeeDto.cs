using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Read;

public class ReadEmployeeDto
{
    public string CompanyName { get; set; }
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Cpf { get; private set; }
    [DataType(DataType.Date)]
    public DateTime? HireDate { get;private set; }
    public int CompanyId {get; private set;}
}