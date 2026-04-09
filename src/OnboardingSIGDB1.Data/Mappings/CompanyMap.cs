using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Mappings;

public class CompanyMap: IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        // configs internas do abstractvalidator
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
      
        
        builder.Property(c => c.FoundationDate)
            .IsRequired(false);
        
        builder.Property(x => x.Cnpj)
            .IsRequired()
            .HasMaxLength(14);
        
        builder.Ignore(x => x.ValidationResult);
        
        // Relacionamento
        
        builder.HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Navigation(x => x.Employees)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        
    }
}


