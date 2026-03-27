using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Mappings;

public class EmployeeAndPositionMap: IEntityTypeConfiguration<EmployeeAndPosition>
{
    public void Configure(EntityTypeBuilder<EmployeeAndPosition> builder)
    {
        builder.ToTable("EmployeeAndPositions");
        
        builder.HasKey(e => new  { e.EmployeeId, e.PositionId });
      
        builder.Ignore(x => x.ValidationResult);
        // ligação com funcionario

        builder.HasOne(e => e.Employee)
            .WithMany(e => e.EmployeeAndPositions)
            .HasForeignKey(e => e.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

       // ligação com cargo

       builder.HasOne(e => e.Position)
           .WithMany(e => e.EmployeeAndPositions)
           .HasForeignKey(e => e.PositionId)
           .OnDelete(DeleteBehavior.Restrict);
       
       
    }
}