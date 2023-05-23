// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using Calliope.Core.Configurations;
using Calliope.Core.Constants;
using Calliope.Core.Converters;
using Calliope.WebAPI.Factories;
using Calliope.WebAPI.Filters.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Templates;
using Serilog.Templates.Themes;

namespace Calliope.WebAPI.Extensions;

/// <summary>
///   Extensions for <see cref="IServiceCollection" />.
/// </summary>
internal static class ServiceCollectionExtensions {
  private static readonly string _xmlFile = Path.Join(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
  private static readonly string _logFile = Path.Join(AppContext.BaseDirectory, "logs", "log-.txt");

  private static readonly ExpressionTemplate _loggerTemplate = new(
    "[{@t:HH:mm:ss} {@l:u3} ({Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)})] {@m}\n{@x}",
    theme: TemplateTheme.Literate
  );

  internal static IServiceCollection ConfigureLayers(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .AddIocLayer(configuration)
      .AddApplicationLayer();

  internal static IServiceCollection ConfigureRouting(this IServiceCollection serviceCollection)
    => serviceCollection.AddRouting(options => options.LowercaseUrls = true);

  internal static IServiceCollection ConfigureProblemDetailsFactory(this IServiceCollection serviceCollection)
    => serviceCollection.AddSingleton<ProblemDetailsFactory, InternalProblemDetailsFactory>();

  internal static IServiceCollection ConfigureLogger(this IServiceCollection serviceCollection)
    => serviceCollection.AddLogging(
      builder => builder
        .ClearProviders()
        .AddSerilog(
          new LoggerConfiguration()
            .WriteTo.File(_logFile, rollingInterval: RollingInterval.Day)
            .WriteTo.Console(_loggerTemplate)
            .CreateLogger()
        )
    );

  internal static IServiceCollection ConfigureJsonSerializers(this IServiceCollection serviceCollection) {
    serviceCollection
      .ConfigureHttpJsonOptions(
        options => {
          options.SerializerOptions.Converters.Add(new EntityIdJsonConverterFactory());
        }
      )
      .AddControllers()
      .AddJsonOptions(
        options => {
          options.JsonSerializerOptions.Converters.Add(new EntityIdJsonConverterFactory());
        }
      );

    return serviceCollection;
  }

  internal static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .AddApiVersioning(
        options => {
          options.DefaultApiVersion = new ApiVersion(1, 0);
          options.AssumeDefaultVersionWhenUnspecified = true;
          options.ReportApiVersions = true;
        }
      )
      .AddVersionedApiExplorer(
        options => {
          options.GroupNameFormat = "'v'VVV";
          options.SubstituteApiVersionInUrl = true;
        }
      )
      .AddSwaggerGen(
        options => {
          var swagger = configuration.GetSection(ConfigurationConstants.SWAGGER_CONFIGURATION_KEY).Get<SwaggerConfiguration>();

          if (swagger is null) {
            throw new InvalidOperationException("Swagger section is not configured.");
          }

          foreach (var version in swagger.Versions) {
            options.SwaggerDoc(
              version, new OpenApiInfo {
                Title = swagger.Title,
                Version = version,
                Description = swagger.Description,
                Contact = new OpenApiContact {
                  Name = swagger.Contact.Name,
                  Email = swagger.Contact.Email,
                  Url = new Uri(swagger.Contact.Url)
                },
                License = new OpenApiLicense {
                  Name = swagger.License.Name,
                  Url = new Uri(swagger.License.Url)
                }
              }
            );
          }

          options.IncludeXmlComments(_xmlFile);

          options.OperationFilter<ParameterIgnoreFilter>();

          options.AddSecurityDefinition(
            "Bearer", new OpenApiSecurityScheme {
              Name = "Authorization",
              Type = SecuritySchemeType.ApiKey,
              Scheme = "Bearer",
              BearerFormat = "JWT",
              In = ParameterLocation.Header,
              Description = """
              JWT Authorization header using the Bearer scheme.<br />
              Enter 'Bearer' [space] and then your token in the text input below.<br />
              Example: "Bearer 12345abcdef"
              """
            }
          );

          options.AddSecurityRequirement(
            new OpenApiSecurityRequirement {
              {
                new OpenApiSecurityScheme {
                  Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                  }
                },
                Array.Empty<string>()
              }
            }
          );
        }
      );
}