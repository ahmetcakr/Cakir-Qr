using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.ElasticSearch;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using QrMenu.Application.Features.Companies.Rules;
using QrMenu.Application.Features.CompanyTypes.Rules;
using QrMenu.Application.Services.AuthenticatorService;
using QrMenu.Application.Services.AuthService;
using QrMenu.Application.Services.CompaniesService;
using QrMenu.Application.Services.UsersService;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Core.QrCodeGenerator.Services;
using Core.QrCodeGenerator.Implementations;
using QrMenu.Application.Features.OperationClaims.Rules;
using QrMenu.Application.Features.UserOperationClaims.Rules;
using QrMenu.Application.Services.OperationClaims;
using QrMenu.Application.Features.Categories.Rules;
using QrMenu.Application.Services.CategoriesService;
using QrMenu.Application.Services.ItemsService;
using QrMenu.Application.Features.Items.Rules;

namespace QrMenu.Application;

public static class ApplicationServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddSingleton<IQrCodeGeneratorService, QrCodeGeneratorService>();
        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<LoggerServiceBase, FileLogger>();
        services.AddSingleton<IElasticSearch, ElasticSearchManager>();
        services.AddScoped<IAuthService, AuthManager>();
        services.AddScoped<IAuthenticatorService, AuthenticatorManager>();
        services.AddScoped<IUserService, UserManager>();
        services.AddScoped<IOperationClaimService, OperationClaimManager>();

        services.AddScoped<ICompanyService, CompanyManager>();
        services.AddScoped<ICompanyTypeService, CompanyTypeManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IItemService, ItemManager>();

        services.AddScoped<CompanyBusinessRules>();
        services.AddScoped<CompanyTypeBusinessRules>();
        services.AddScoped<CompanyTypeBusinessRules>();
        services.AddScoped<CompanyBusinessRules>();
        services.AddScoped<OperationClaimBusinessRules>();
        services.AddScoped<UserOperationClaimBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddScoped<ItemBusinessRules>();
    }

    public static IServiceCollection AddSubClassesOfType(
    this IServiceCollection services,
    Assembly assembly,
    Type type,
    Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (Type? item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);
            else
                addWithLifeCycle(services, type);
        return services;
    }
}
