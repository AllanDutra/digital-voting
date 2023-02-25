using DigitalVoting.Core.Interfaces;
using DigitalVoting.Infrastructure.Persistence.Repositories;

namespace DigitalVoting.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IVoterRepository, VoterRepository>();

            return services;
        }
    }
}