using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Notifications;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace OnboardingSIGDB1.Domain.Models;

[Table("Positions")]
public class Position : BaseEntity<Position>
{
    
    public string Description { get;private set; }
    public string Name { get;private set; }
    public ICollection<EmployeeAndPosition> EmployeeAndPositions { get;private set; }
    public ValidationResult ValidationResult { get; private set; }
    
    
    protected Position(){}
    
    public Position(string description)
    {
        Description = description;
        
    }
    
    public override bool Validate()
    {
        ClearNotifications();

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(100).WithMessage("Description must not exceed 100 characters.");
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        ValidationResult = Validate(this);
        
        return ValidationResult.IsValid;
    }
}