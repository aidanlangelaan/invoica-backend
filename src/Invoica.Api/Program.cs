using Invoica.Api.Endpoints;
using Invoica.Api.Extensions;
using Invoica.Api.Middleware;
using Invoica.Api.OpenApi;
using Invoica.Application.Extensions;
using Invoica.Infrastructure.Extensions;
using Invoica.Persistence.Extensions;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

builder.Services
    .RegisterApiServices()
    .RegisterApplicationServices(builder.Configuration)
    .RegisterInfrastructureServices(builder.Configuration)
    .RegisterPersistenceServices(builder.Configuration);

var authSettings = builder.Configuration.GetSection("Authentication");
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = authSettings["Authority"];
        options.Audience = authSettings["Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = authSettings["Audience"]
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddProblemDetails();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Invoica - Backend";
    config.Version = "v1";
    config.Description = "Backend API for Invoica - the online invoicing application.";

    config.AddSecurity("Bearer", [], new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Input your JWT token in this format: Bearer {your token}."
    });

    config.PostProcess = doc =>
    {
        doc.Info.Contact = new OpenApiContact
        {
            Name = "Aidan Langelaan",
            Email = "aidan@langelaan.pro"
        };
    };

    config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserIdentificationMiddleware>();

// Enable OpenAPI and Scalar API reference
app.UseOpenApi();
app.MapOpenApi();
app.MapScalarApiReference((options, _) =>
{
    options.Title = "Invoica - Backend";
    options.Theme = ScalarTheme.Laserwave;
    options.DefaultHttpClient = new KeyValuePair<ScalarTarget, ScalarClient>(ScalarTarget.Node, ScalarClient.Axios);
    options.WithDocumentDownloadType(DocumentDownloadType.Json);
    options.WithClientButton(false);
    options.AddPreferredSecuritySchemes("BearerAuth");
    options.WithPersistentAuthentication();
});

// Group and map endpoints
app.MapAccountEndpoints();
app.MapUserEndpoints();

app.Run();

public partial class Program { }

