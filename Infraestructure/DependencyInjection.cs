using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Infraestructure.Services;
using Application.Interfaces.Services;
using Application.Interfaces.Infraestructure.Services;

namespace Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IHackerNewsService, HackerNewsService>();

            return services;
        }
    }
}
