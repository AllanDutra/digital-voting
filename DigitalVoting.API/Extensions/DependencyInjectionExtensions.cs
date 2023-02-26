using DigitalVoting.Core.Interfaces.Repositories;
using DigitalVoting.Infrastructure.Persistence.Repositories;

namespace DigitalVoting.API.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IVoterRepository, VoterRepository>();
            services.AddScoped<IPollRepository, PollRepository>();

            return services;
        }
    }
}