using QrMenu.Application.Repositories;
using QrMenu.Persistence.Contexts;
using QrMenu.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QrMenu.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:BaseDb"]));

        using (var scope = services.BuildServiceProvider().CreateScope())
        {
            using (var dbContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>())
            {
                try
                {
                    if (!dbContext.Database.CanConnect())
                    {
                        dbContext.Database.EnsureCreated();
                    }
                    
                    if (dbContext.Database.GetPendingMigrations().Any())
                    {
                        dbContext.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama yapılabilir
                    Console.WriteLine($"Veritabanı migration hatası: {ex.Message}");
                }
            }
        }

        services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
        services.AddScoped<IOperationClaimRepository, OperationClaimRepository>();
        services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemImageRepository, ItemImageRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IMenuQrCodeRepository, MenuQrCodeRepository>();

        return services;
    }
}
