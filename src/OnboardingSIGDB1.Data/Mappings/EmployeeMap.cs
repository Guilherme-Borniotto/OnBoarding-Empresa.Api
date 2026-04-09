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
            .HasMaxLength(150);

        builder.Ignore(e => e.ValidationResult);
        
        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasMaxLength(11);
        
        builder.Property(x => x.HireDate)
            .IsRequired(false);
        
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        
        // Relacionamento 1:n
        
        builder.HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        //Relacionamento N:1
        // significa que o processo  deletar do employee pode deletar a relação com vinculo
        builder.HasMany(e => e.EmployeeAndPositions)
            .WithOne(e => e.Employee)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        
            
}    }
