using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace OnboardingSIGDB1.Domain.Models;


public class Employee : BaseEntity<Employee>
{

   
     public string Name { get; private set; }
     public string Cpf { get; private set; }
     public DateTime? HireDate { get;private set; }
     public Company Company{get; private set;}
     public int? CompanyId {get; private set;}
     public ICollection<EmployeeAndPosition> EmployeeAndPositions { get; private set; }
     public ValidationResult ValidationResult { get; private set; }
    

    protected Employee() { }
    
    public Employee(string name, string cpf, DateTime? hiredate) 
    {
      Name = name;
      Cpf = cpf;
      HireDate = hiredate;
    }

    public override bool Validate()
    {
        ClearNotifications();

        // Regras de Domínio
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

        RuleFor(e => e.Cpf)
            .NotEmpty().WithMessage("CPF is required.")
            .Length(11).WithMessage("CPF must be exactly 11 characters.");

        RuleFor(e => e.HireDate)
            .NotEmpty().WithMessage("Hiring date is required.")
            .GreaterThan(DateTime.MinValue).WithMessage("Hiring date must be a valid date.");

        RuleFor(e => e.CompanyId)
            .GreaterThan(0).WithMessage("Employee must be linked to a valid company.");

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


    public Task AddingPosition( Position position, DateTime date)
    {
        EmployeeAndPositions.Add(new EmployeeAndPosition(this, position, date));
        return Task.CompletedTask;
        
    }




}


