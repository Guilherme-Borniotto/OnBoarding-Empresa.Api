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
    public DateTime? HireDate { get; private set; }
    public int CompanyId { get; private set; } 
    public Company Company { get; private set; }
    public ICollection<EmployeeAndPosition>? EmployeeAndPositions { get; private set; }
    public ValidationResult ValidationResult { get; private set; }

    protected Employee() { }

    public Employee(string name, string cpf, DateTime? hireDate, int companyId)
    {
        Name = name;
        Cpf = StringUtils.removemask(cpf);
        HireDate = hireDate;
        CompanyId = companyId;
    }

    public override bool Validate()
    {
        ClearNotifications();
        
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

        RuleFor(e => e.Cpf)
            .NotEmpty().WithMessage("CPF is required.")
            .Length(11).WithMessage("CPF must be exactly 11 characters.")
            .Must(CpfUtils.IsValid).WithMessage("CPF is invalid.");

        RuleFor(e => e.HireDate)
            
            .Must(d => !d.HasValue || d.Value > DateTime.MinValue)
            .WithMessage("Hiring date is invalid.")

           
            .Must(d => !d.HasValue || d.Value.Date <= DateTime.Today)
            .WithMessage("Hiring date cannot be in the future.");
        
        RuleFor(e => e.CompanyId)
            .NotEmpty().WithMessage("Company Id must be provided.") 
            .GreaterThan(0).WithMessage("Employee must be linked to a valid company.");
        
        ValidationResult = Validate(this);

        return ValidationResult.IsValid;
    }
}


