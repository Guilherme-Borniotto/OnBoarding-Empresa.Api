using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Read;

public class ReadEmployeeAndPositionDto
{
    public bool Situation { get; set; }
    public int EmployeeId{get; private set;}
    public string NameEmployee{get; private set;}
    public int PositionId { get; private set; }
    public string PositionName{get; private set;}
    
    [DataType(DataType.Date)]
    public DateTime DatePosition { get; private  set; }
}