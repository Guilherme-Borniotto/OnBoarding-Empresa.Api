using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Update;

public class UpdateEmployeeAndPositionDto
{
    [DataType(DataType.Date)]
    public DateTime? DatePosition { get; set; }
    public bool Situation { get;  set; }
}