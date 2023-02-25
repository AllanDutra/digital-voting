using DigitalVoting.API.Middlewares;

namespace DigitalVoting.API.Extensions
{
    public static class MiddlewaresExtensions
    {
        public static IServiceCollection AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient<GlobalExceptionHandler>();

            return services;
        }
    }
}