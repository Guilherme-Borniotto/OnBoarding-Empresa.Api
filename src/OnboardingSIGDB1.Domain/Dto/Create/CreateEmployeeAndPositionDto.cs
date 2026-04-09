using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Create;

public class CreateEmployeeAndPositionDto
{
    public int EmployeeId{get;  set;}
    public int PositionId { get;  set; }
    [DataType(DataType.Date)]
    public DateTime? DatePosition { get;   set; }
}