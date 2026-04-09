using System.ComponentModel.DataAnnotations;

namespace OnboardingSIGDB1.Domain.Dto.Update;

public class UpdateEmployeeDto
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    [DataType(DataType.Date)]
    public DateTime? HireDate { get; set; }
   
}