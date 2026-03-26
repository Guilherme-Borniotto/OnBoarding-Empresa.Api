namespace OnboardingSIGDB1.Domain.Dto;

public class EmployeeAndPositionFilter
{
    public int? EmployeeId{get; private set;}
    public int? PositionId { get; set; }
    public DateTime? DatePosition { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}