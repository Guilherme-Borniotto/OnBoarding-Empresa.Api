using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.Domain.Models;

public class EmployeeAndPosition :BaseCompostKey<EmployeeAndPosition>
{

    public int Id{get; set;}
    public Employee Employee { get; private set; }
    public Position Position { get; private set; }
    public int EmployeeId{get; private set;}
    public int PositionId { get; private set; }
    public DateTime DatePosition { get; private  set; }
    public ValidationResult ValidationResult { get; private set; }
    
    public  EmployeeAndPosition()
    {}

    public EmployeeAndPosition(Employee employee, Position position, DateTime datePosition)
    {
        EmployeeId = employee.Id;
        PositionId = position.Id;
        DatePosition = datePosition;
        Employee = employee;
        Position = position;
    }


    public override bool Validate()
    {
        RuleFor(ep => ep.EmployeeId)
            .NotEmpty().WithMessage("EmployeeId is required");

        RuleFor(ep => ep.PositionId)
            .NotEmpty().WithMessage("PositionId is required");

        RuleFor(ep => ep.DatePosition)
            .Must(d => d > DateTime.MinValue).WithMessage("Foundation date must be a valid date.");

        ValidationResult = Validate(this);

        if (!ValidationResult.IsValid)
        {
            foreach (var error in ValidationResult.Errors)
            {
                AddNotification(error.PropertyName, error.ErrorMessage);
            }
        }
        
        return ValidationResult.IsValid;
    }
 
}