using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Models;


public class Employee : Notifiable
{

     public int Id { get; private set; }
     public string Name { get; private set; }
     public string Cpf { get; private set; }
     public DateTime? HireDate { get;private set; }
     public DateTime CreatedAtEmployee { get; private set; }
     public Company Company{get; private set;}
     public int? CompanyId {get; private set;}

     public ICollection<EmployeeAndPosition> EmployeeAndPositions { get; private set; }
    

    protected Employee() { }
    
    public Employee(string name, string cpf, DateTime? hiredate)
    {
        SetName(name);
        SetCpf(cpf);
        SetHireDate(hiredate);
        CreatedAtEmployee = DateTime.Now;
    }

    // Validação/Linkagem de empresa
    public void LinkCompany()
    {
        if (CompanyId.HasValue)
            AddNotification("Company", "Employee is already linked to a company");
    }

    // Métodos privados para validação e atribuição
    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            AddNotification("Name", "Name is required.");
        else if (name.Length > 150)
            AddNotification("Name", "Maximum 150 characters.");
        else
            Name = name.Trim();
    }

    private void SetCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            AddNotification("Cpf", "CPF is required.");
            return;
        }

        string onlyNumbers = StringUtils.removemask(cpf);

        if (!CpfUtils.IsValid(onlyNumbers)) //  validação de CPF
        {
            AddNotification("Cpf", "Invalid CPF.");
            return;
        }

        if (onlyNumbers.Length != 11)
        {
            AddNotification("Cpf", "CPF must have 11 digits.");
            return;
        }

        Cpf = onlyNumbers;
    }

    private void SetHireDate(DateTime? hireDate)
    {
        
        if(hireDate.HasValue) return;
        
        if (hireDate == DateTime.MinValue || hireDate > DateTime.UtcNow)
            AddNotification("HireDate", "Invalid hire date.");
        else
            HireDate = hireDate;
    }
}


