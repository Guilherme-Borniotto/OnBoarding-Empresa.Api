using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Domain.Dto;

public class PositionFilter
{
    public string? Description { get;private set; }
    public string? Name { get;private set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}