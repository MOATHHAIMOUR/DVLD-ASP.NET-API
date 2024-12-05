using DVLD.Domain.IRepository;
using DVLD.Domain.IRepository.Base;
using DVLD.Infrastructure.Repository;
using DVLD.Infrastructure.Repository.Base;
using DVLD.Infrastructure.Repository.Base.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DVLD.Infrastructure
{
    public static class RegisterInfrastructureDependencies
    {
        public static IServiceCollection InfrastructureDependencies(this IServiceCollection services)
        {


            // Add Auto Mapper in DI contianer
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILocalDrivingApplicationRepository, LocalDrivingApplicationRepository>();
            services.AddScoped<ITestAppointmentRepository, TestAppointmentRepository>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<IDBViewsRepository, DBViewsRepository>();
            services.AddScoped<IInternationalLicenseRepository, InternationalLicenseRepository>();
            services.AddScoped<IDetainedLicenseRepository, DetainedLicenseRepository>();

            return services;
        }
    }
}
