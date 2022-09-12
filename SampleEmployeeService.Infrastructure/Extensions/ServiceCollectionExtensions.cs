using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Contexts;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using SampleEmployeeService.Infrastructure.Persistence;
using SampleEmployeeService.Infrastructure.Persistence.Repositories;
using SampleEmployeeService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistenceContexts()
                .AddRepositories()
                .AddSharedServices()
                .AddApplicationInsightsTelemetry()
                .AddCustomHealthChecks();
        }
        private static IServiceCollection AddPersistenceContexts(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ISampleEmployeeServiceDbContext, SampleEmployeeServiceDbContext>();

            return services;
        }
        private static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddDbContextCheck<SampleEmployeeServiceDbContext>();

            return services;
        }
        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
        private static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTimeService, DateTimeService>();
            return services;
        }

     }
}
