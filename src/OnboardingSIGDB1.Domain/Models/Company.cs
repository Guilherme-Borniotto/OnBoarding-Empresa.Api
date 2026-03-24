using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Models;

[Table("Companies")]
public class Company : Notifiable
{
   public int Id { get; private set; }
    public string Name { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime? Foundation { get; private set; }
    public DateTime CreatedAtCompany { get; private set; }

    private readonly List<Employee> _employees = new();
    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    protected Company() { }

    public Company(string name, string cnpj, DateTime? foundationDate)
    {
        SetName(name);
        SetCnpj(cnpj);
        SetFoundationDate(foundationDate);
        CreatedAtCompany = DateTime.UtcNow;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            AddNotification("Name", "Name is required.");
        else if (name.Length > 150)
            AddNotification("Name", "Company name must not exceed 150 characters.");
        else
            Name = name.Trim();
    }
    private void SetCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
        {
            AddNotification("Cnpj", "CNPJ is required.");
            return;
        }

        string onlyNumbers = StringUtils.removemask(cnpj);

        if (!CnpjUtils.IsValid(onlyNumbers))
        {
            AddNotification("Cnpj", "Invalid CNPJ.");
            return;
        }

        if (onlyNumbers.Length != 14)
        {
            AddNotification("Cnpj", "CNPJ must have 14 digits.");
            return;
        }

        Cnpj = onlyNumbers;
    }

    private void SetFoundationDate(DateTime? foundationDate)
    {

        if (!foundationDate.HasValue)  return;
        
        
        if (foundationDate == DateTime.MinValue)
            AddNotification("FoundationDate", "Foundation date is required.");
        else if (foundationDate > DateTime.UtcNow)
            AddNotification("FoundationDate", "Foundation date cannot be in the future.");
        else
            Foundation = foundationDate;
    }

    // Adicionar funcionário à empresa
    public void AddEmployee(Employee employee)
    {
        if (employee == null) return;
        _employees.Add(employee);
    }
}