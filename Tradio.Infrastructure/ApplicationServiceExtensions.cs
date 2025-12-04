using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Tradio.Application.Services;
using Tradio.Application.Services.ApplicationUserServices;
using Tradio.Application.Services.Categories;
using Tradio.Application.Services.Cities;
using Tradio.Application.Services.Climates;
using Tradio.Application.Services.ComplaintReplies;
using Tradio.Application.Services.Complaints;
using Tradio.Application.Services.Countries;
using Tradio.Application.Services.Messages;
using Tradio.Application.Services.Payments;
using Tradio.Application.Services.Services;
using Tradio.Application.Services.SubscriptionTypes;
using Tradio.Application.Services.UserSubscriptionService;
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
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
            services.AddScoped<ISubscriptionTypeService, SubscriptionTypeService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IComplaintService, ComplaintService>();
            services.AddScoped<IComplaintReplyService, ComplaintReplyService>();
            services.AddScoped<IApplicationUserServiceService, ApplicationUserServiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IClimateService, Application.Services.Climates.ClimateService>();

            return services;
        }
    }
}
