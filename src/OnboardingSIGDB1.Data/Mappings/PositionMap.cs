using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Mappings;

public class PositionMap : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        
        builder.ToTable("Positions");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        
        builder.Ignore(x => x.ValidationResult);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);
        
        // Relacionamento
        
        builder.HasMany(p => p.EmployeeAndPositions)
            .WithOne(e => e.Position)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}