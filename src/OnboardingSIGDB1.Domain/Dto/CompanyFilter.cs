namespace OnboardingSIGDB1.Domain.Dto;

public class CompanyFilter
{
    public string? Name { get; private set; }
    public string? Cnpj { get; private set; }
    public DateTime? Foundation { get; private set; }
    public DateTime? Deadline { get; private set; }
    public DateTime? CreatedAtCompany { get; private set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}