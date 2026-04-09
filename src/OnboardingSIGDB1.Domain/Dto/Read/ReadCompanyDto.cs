using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Read;

public class ReadCompanyDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    
    [DataType(DataType.Date)]
    public DateTime? FoundationDate { get; set; }
}   