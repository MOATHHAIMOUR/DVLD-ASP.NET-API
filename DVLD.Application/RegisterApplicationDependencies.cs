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

            // Add Fluent Validation in DI contianer
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // Add Auto Mapper in DI contianer
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add All  DI contianer
            services.AddScoped<IPersonServices, PersonServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ISharedServices, SharedServices>();
            services.AddScoped<ILocalDrivingLicenseApplicationServices, LocalDrivingApplicationServices>();
            services.AddScoped<ITestServices, TestServices>();
            services.AddScoped<IInternationalLicenseServices, InternationalLicenseServices>();


            return services;
        }
    }
}
