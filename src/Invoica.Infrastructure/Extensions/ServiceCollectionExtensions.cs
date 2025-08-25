using Invoica.Application.Common.Interfaces;
using Invoica.Infrastructure.Identity.Services;
using Invoica.Infrastructure.Services.FileStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoica.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<FileStorageSettings>()
            .Configure(options => configuration.GetSection("FileStorageSettings")
                .Bind(options));

        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<IUserProvisioningService, UserProvisioningService>();
        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
