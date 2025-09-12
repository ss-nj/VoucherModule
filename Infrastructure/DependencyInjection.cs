using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories.Base;
using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");

            services.AddDbContext<VoucherModuleDbContext>(options =>
                options.UseSqlServer(connectionString));

            // register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IMasterRepository, MasterRepository>();


            return services;
        }     

    }
}
