using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Update;

public class UpdateCompanyDto
{
    public string Name { get; set; }
    public string Cnpj { get; set; }
    [DataType(DataType.Date)]
    public DateTime? FoundationDate { get; set; }
}