using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Create;

public class CreateCompanyDto
{
    public string Name { get;  set; }
    public string Cnpj { get;  set; }
    [DataType(DataType.Date)]
    public DateTime? FoundationDate { get; set; }
}