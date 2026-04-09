
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Utils;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace OnboardingSIGDB1.Domain.Models;

public class Company : BaseEntity<Company>
{
    public ICollection<Employee> Employees { get; private set; }
    
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime? FoundationDate { get; private set; }
    public ValidationResult ValidationResult { get; private set; }
    protected Company() { }
 
    public Company(string name, string cnpj, DateTime? foundationDate)
    {
        Name = name;
        Cnpj = StringUtils.removemask(cnpj);
        FoundationDate = foundationDate;
    }
 
    public override bool Validate()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");
            
        RuleFor(c => c.Cnpj)
            .NotEmpty().WithMessage("CNPJ is required.")
            .Length(14).WithMessage("CNPJ must be exactly 14 characters.")
            .Must(CnpjUtils.IsValid).WithMessage("CNPJ is invalid.");
        
        RuleFor(c => c.FoundationDate)
           
            .Must(d => !d.HasValue || d.Value > DateTime.MinValue)
            .WithMessage("Foundation date is invalid.")

            
            .Must(d => !d.HasValue || d.Value.Date <= DateTime.Today)
            .WithMessage("Foundation date cannot be in the future.");
 
        ValidationResult = Validate(this);
        
        return ValidationResult.IsValid;
    }
 
    
}