using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Mappings;

public class EmployeeMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");
        
        builder.HasKey(x => x.Id);
        builder.Property(x=> x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Cpf)
            .IsRequired();
        
        builder.Property(x => x.HireDate)
            .IsRequired();
        
        
        // Relacionamento 1:n
        
        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //Relacionamento N:1
        
        builder.HasMany(e => e.EmployeeAndPositions)
            .WithOne(e => e.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
            
}    }
