using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnboardingSIGDB1.Data.Mappings;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }


    // DBSETs

    public DbSet<Company> Companies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<EmployeeAndPosition> EmployeeAndPositions { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Aplicar todos os mapeamentos
        modelBuilder.ApplyConfiguration(new CompanyMap());
        modelBuilder.ApplyConfiguration(new EmployeeMap());
        modelBuilder.ApplyConfiguration(new PositionMap());
        modelBuilder.ApplyConfiguration(new EmployeeAndPositionMap());

        base.OnModelCreating(modelBuilder);


    }
}