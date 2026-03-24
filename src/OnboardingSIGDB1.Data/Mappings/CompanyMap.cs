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
            .HasMaxLength(50);

        builder.Property(c => c.Foundation)
            .IsRequired();
        
        builder.Property(x => x.Cnpj)
            .IsRequired();
        
        // Relacionamento
        
        builder.HasMany(c => c.Employees)
            .WithOne(e => e.Company)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
    }
}


