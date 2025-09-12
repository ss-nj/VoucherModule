using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using Application.MappingProfiles;
using Application.Interfaces;
using Application.Services;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperConfig).Assembly);

            // register application services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<ISubsidiaryService, SubsidiaryService>();
      

            return services;
        }
    }
}
