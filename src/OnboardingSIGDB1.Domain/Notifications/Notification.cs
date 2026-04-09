using System.Dynamic;

namespace OnboardingSIGDB1.Domain.Notifications;

public class Notification
{
    
    public string field  { get; set; }
    public string Description { get; set; }

    public Notification(){}
    public Notification(string field, string description)
    {
       this.field = field;
       Description = description;
    }
}