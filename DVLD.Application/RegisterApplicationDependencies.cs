using DVLD.Application.Common.Behaviours;
using DVLD.Application.Services;
using DVLD.Application.Services.IServices;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DVLD.Application
{
    public static class RegisterApplicationDependencies
    {
        public static IServiceCollection ApplicationDependencies(this IServiceCollection services)
        {
            // Add MediaR in DI contianer
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<ISharedServices, SharedServices>();


            return services;
        }
    }
}
