using FluentValidation;
using Invoica.Api.Common.Validators;
using Invoica.Api.ViewModels.Account.Mapping;
using Invoica.Application.Common.Interfaces;
using Invoica.Infrastructure.Services;

namespace Invoica.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApiServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddHttpContextAccessor();

        // API specific current user service
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddValidatorsFromAssemblyContaining<PagedRequestValidator>();

        services.AddSingleton<AccountViewModelMapper>();

        return services;
    }
}
