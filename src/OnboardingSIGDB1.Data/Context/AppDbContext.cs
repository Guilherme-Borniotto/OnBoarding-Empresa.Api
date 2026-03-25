using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnboardingSIGDB1.Data.Mappings;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Notifications;

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
        modelBuilder.Ignore<Notification>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);


    }
}