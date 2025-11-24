using BCT.BusinessRule.LogicServices;
using BCT.CommonLib.Services;
using BCT.BusinessRule.Services.HangfireServices;
using BCT.DataAccess.Data;
using BCT.DataAccess.DataRepositories;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace BCT.BookingSystem.Entity.RedisService;

public class RedisConfigureSetting(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        services.AddScoped<User>();
        services.AddScoped<BookingService>();
        services.AddScoped<Booking>();
        services.AddScoped<PaymentService>();
        services.AddScoped<Payment>();
        services.AddScoped<ResponseService>();
        services.AddScoped<IRedisCacheService, RedisCacheService>();
        services.AddDbContext<BookingSystemDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("BookingSystemConnectionString"),
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });
        services.AddStackExchangeRedisCache(options =>
        {
            var host = configuration.GetValue<string>("RedisCache:Host");
            var port = configuration.GetValue<int>("RedisCache:Port");
            options.Configuration = $"{host}:{port}";
        });
        services.AddHangfire(x =>
        x.UseSqlServerStorage(configuration.GetConnectionString("BookingSystemConnectionString")));
        services.AddHangfireServer();
        services.AddScoped<HangfireEmailService>();
        services.AddScoped<NotificationJobService>();
        services.AddScoped<AuditLog>();
    }
}
