using DVLD.Domain.IRepository;
using DVLD.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DVLD.Infrastructure
{
    public static class RegisterInfrastructureDependencies
    {
        public static IServiceCollection InfrastructureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ISharedRepository, SharedRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
