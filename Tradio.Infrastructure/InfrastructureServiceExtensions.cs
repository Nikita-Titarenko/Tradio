using Eventa.Application.Repositories;
using Eventa.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;
using Tradio.Application.Services;
using Tradio.Infrastructure.Options;
using Tradio.Infrastructure.Services;
namespace Tradio.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password = new PasswordOptions
                {
                    RequireNonAlphanumeric = false
                };
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IPaymentService, StripePaymentService>();
            services.AddScoped<IFileService, Services.FileService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.Configure<SmtpEmailOptions>(configuration.GetSection("EmailOptions"));
            services.Configure<JwtTokenOptions>(configuration.GetSection("Jwt"));
            services.Configure<PaymentOptions>(configuration.GetSection("Stripe"));

            var options = configuration.GetSection("Stripe");
            StripeConfiguration.ApiKey = options["SecretKey"];

            return services;
        }
    }
}
