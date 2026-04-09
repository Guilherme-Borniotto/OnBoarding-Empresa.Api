using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Models;

public class EmployeeAndPosition :BaseCompostKey<EmployeeAndPosition>
{
    public Employee Employee { get; private set; }
    public Position Position { get; private set; }
    public bool Situation { get; private set; } = true;
    public int EmployeeId{get; private set;}
    public int PositionId { get; private set; }
    public DateTime DatePosition { get; private set; }
    public ValidationResult ValidationResult { get; private set; }
    
    protected EmployeeAndPosition()
    {}

    public EmployeeAndPosition(Employee employee, Position position, DateTime datePosition)
    {
        EmployeeId = employee.Id;
        PositionId = position.Id;
        DatePosition = datePosition;
        Employee = employee;
        Position = position;
    }


    public void changeSituationForFalse()
    {
        Situation = false;
    }

    public void changeSituationForTrue()
    {
        Situation = true;
    }
    
    
    
    public override bool Validate()
    {
        RuleFor(ep => ep.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId is required");

        RuleFor(ep => ep.PositionId)
            .NotEmpty().WithMessage("PositionId is required");

        RuleFor(ep => ep.DatePosition)
            .Must(d => d > DateTime.MinValue).WithMessage("Foundation date must be a valid date.")
            .Must(d => d.Date <= DateTime.Today).WithMessage("Foundation date cannot be in the future.");

        ValidationResult = Validate(this);
        
        return ValidationResult.IsValid;
    }
 
}