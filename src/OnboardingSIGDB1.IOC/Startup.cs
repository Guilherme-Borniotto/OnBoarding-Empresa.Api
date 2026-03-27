using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Interfaces;


namespace OnboardingSIGDB1.IOC
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // --- DbContext ---
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // --- UnitOfWork ---
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- Repositórios ---
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();

            // --- Services ---
          //  services.AddScoped<CompanyService>();
          //  services.AddScoped<EmployeeService>();
           // services.AddScoped<PositionService>();
        }
    }
}