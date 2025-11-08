using Microsoft.Extensions.DependencyInjection;
using Tradio.Application.Services;
using Tradio.Infrastructure.Services;
namespace Tradio.Infrastructure
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();


            return services;
        }
    }
}
