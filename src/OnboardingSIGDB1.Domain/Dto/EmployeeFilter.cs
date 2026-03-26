namespace OnboardingSIGDB1.Domain.Dto;

public class EmployeeFilter
{
        public string? Name { get; set; }
        public string? Cpf { get; set; }
        public DateTime? HiredFrom { get; set; }
        public DateTime? HiredTo { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    
   }