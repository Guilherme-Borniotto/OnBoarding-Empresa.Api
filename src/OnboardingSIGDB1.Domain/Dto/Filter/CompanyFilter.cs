using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto;

public class CompanyFilter
{
    public string? Name { get;set; }
    public string? Cnpj { get; set; }
 
    [DataType(DataType.Date)]
    public DateTime? Foundation { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Deadline { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}