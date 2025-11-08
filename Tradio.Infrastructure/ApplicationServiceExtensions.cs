using Microsoft.Extensions.DependencyInjection;
using Tradio.Application.Services;
using Tradio.Application.Services.Categories;
using Tradio.Application.Services.Cities;
using Tradio.Application.Services.Countries;
using Tradio.Infrastructure.Services;
namespace Tradio.Infrastructure
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
