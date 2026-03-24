using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Models;

[Table("Positions")]
public class Position : Notifiable
{
    [Key]
    public int Id { get;private set; } 
    [Required]
    public string Description { get;private set; }
    [Required]
    public string Name { get;private set; }

    public ICollection<EmployeeAndPosition> EmployeeAndPositions { get;private set; }
    
    protected Position(){}
    
    public Position(string description)
    {
        Description = description;
        
    }

    public void SetNome()
    {
        if (string.IsNullOrWhiteSpace(Name))
            AddNotification("Name", "Name is required");

        if (Name?.Length > 100)
            AddNotification("Name", "Maximum 100 characters");
    }


    public void SetDescription()
    {
        if (Description?.Length > 255)
            AddNotification("Description", "Maximum 255 characters");
    }

}