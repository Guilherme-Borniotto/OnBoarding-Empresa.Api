using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Models;

public class EmployeeAndPosition : Notifiable
{

    public int Id{get; set;}
    public Employee Employee { get; private set; }
    public Position Position { get; private set; }
    public int EmployeeId{get; private set;}
    public int PositionId { get; set; }
    public DateTime DatePosition { get; set; }


    public EmployeeAndPosition(int employeId, int positionId, DateTime datePosition)
    {
        EmployeeId = employeId;
        PositionId = positionId;
        DatePosition = datePosition;
    }
    
    
    
}