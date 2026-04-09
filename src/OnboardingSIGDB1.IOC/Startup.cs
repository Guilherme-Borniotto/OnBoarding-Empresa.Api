using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Context;
using OnboardingSIGDB1.Data.Repositories;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;


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

            //-------- Notificationscontext -----
            services.AddScoped<INotificationContext, NotificationContext>();
            
            // ----AutoMapper-----
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // --- UnitOfWork ---
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // --- Repositórios ---
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeAndPositionRepository, EmployeeAndPositionRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();

            // --- Services ---
            services.AddScoped<ICompanyService,CompanyService>();
            services.AddScoped<IEmployeeService,EmployeeService>();
            services.AddScoped<IPositionservice,PositionService>();
        }
    }
}