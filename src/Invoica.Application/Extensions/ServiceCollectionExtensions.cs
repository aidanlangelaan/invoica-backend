using FluentValidation;
using Invoica.Application.Accounts.Interfaces;
using Invoica.Application.Accounts.Mapping;
using Invoica.Application.Accounts.Services;
using Invoica.Application.Accounts.Validators;
using Invoica.Application.Common.Interfaces;
using Invoica.Application.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoica.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration _)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddValidatorsFromAssemblyContaining<CreateAccountValidator>();

        // services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IPagingService, PagingService>();

        // mapping
        services.AddSingleton<AccountMapper>();

        return services;
    }
}
