using System.Text;
using System.Text.Json;
using FluentResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Tradio.Infrastructure;
using Tradio.Infrastructure.Hubs;
using Tradio.Server.ValidationErrors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var token = builder.Configuration.GetSection("Jwt:Key").Value ?? throw new InvalidOperationException("Jwt key not found");
var issuer = builder.Configuration.GetSection("Jwt:Issuer").Value ?? throw new InvalidOperationException("Jwt key not found");
var audience = builder.Configuration.GetSection("Jwt:Audience").Value ?? throw new InvalidOperationException("Jwt key not found");

builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
            ValidIssuer = issuer,
            ValidAudience = audience
        };
    });

builder.Services.Configure<ApiBehaviorOptions>(opt =>
{
    opt.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(ms => ms.Value != null && ms.Value.Errors.Count > 0)
            .SelectMany(ms => ms.Value!.Errors.Select(e =>
            {
                var baseError = JsonSerializer.Deserialize<BaseValidationError>(e.ErrorMessage);
                if (baseError == null)
                {
                    return new BaseValidationError
                    {
                        Code = "UnknownError",
                        Message = e.ErrorMessage
                    };
                }

                return baseError;
            }));
        return new BadRequestObjectResult(new { errors });
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNgrok", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(origin => true);
    });
});

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(c => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapHub<ChatHub>("/chatHub");

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await DbInitializer.InitializeAsync(context);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseDefaultFiles();

//app.UseHttpsRedirection();

app.UseCors("AllowNgrok");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
