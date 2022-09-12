using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using SampleEmployeeService.ApplicationLayer.Authorization.Extensions.DependencyInjection;
using SampleEmployeeService.ApplicationLayer.Common.Behaviours;

namespace SampleEmployeeService.ApplicationLayer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddDomainAuthorization();
        }

        private static IServiceCollection AddDomainAuthorization(this IServiceCollection services)
        {
            services
                .AddMediatorAuthorization(Assembly.GetExecutingAssembly())
                .AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
