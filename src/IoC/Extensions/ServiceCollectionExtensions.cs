// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using System.Text;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Attributes;
using Calliope.Core.Configurations;
using Calliope.Core.Constants;
using Calliope.IoC.Abstractions.SeedWork;
using Calliope.IoC.Constraints;
using Calliope.IoC.SeedWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Calliope.IoC.Extensions;

/// <summary>
///   Extensions for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions {
  private static readonly Assembly _applicationAssembly = AppDomain.CurrentDomain
    .GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Calliope.Application") ?? Assembly.GetExecutingAssembly();

  private static readonly Assembly _coreAssembly = AppDomain.CurrentDomain
    .GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Calliope.Core") ?? Assembly.GetExecutingAssembly();

  private static readonly Assembly _iocAssembly = AppDomain.CurrentDomain
    .GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Calliope.IoC") ?? Assembly.GetExecutingAssembly();

  /// <summary>
  ///   Add the IoC layer to the <see cref="IServiceCollection" />.
  /// </summary>
  /// <param name="serviceCollection">The <see cref="IServiceCollection" />.</param>
  /// <param name="configuration">The <see cref="IConfiguration" />.</param>
  public static IServiceCollection AddIocLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .ConfigureOptions(configuration)
      .ConfigureDatabaseContext(configuration)
      .ConfigureRepositories()
      .ConfigureProviders()
      .ConfigureRouteConstraints()
      .ConfigureAuthentication(configuration);

  internal static IServiceCollection ConfigureOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    => serviceCollection
      .Configure<JsonWebTokenConfiguration>(configuration.GetSection(ConfigurationConstants.JSON_WEB_TOKEN_CONFIGURATION_KEY));

  internal static IServiceCollection ConfigureDatabaseContext(this IServiceCollection serviceCollection, IConfiguration configuration) {
    var connectionString = configuration.GetConnectionString("MSSQL");

    serviceCollection.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
    serviceCollection
      .AddScoped<IDatabaseContext>(serviceProvider => serviceProvider.GetService<DatabaseContext>()!)
      .AddScoped<IUnitOfWork, UnitOfWork>();

    return serviceCollection;
  }

  internal static IServiceCollection ConfigureAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration) {
    var tokenConfiguration = configuration.GetSection(ConfigurationConstants.JSON_WEB_TOKEN_CONFIGURATION_KEY).Get<JsonWebTokenConfiguration>();

    if (tokenConfiguration is null) {
      return serviceCollection;
    }

    serviceCollection
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        options => options.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = tokenConfiguration.Issuer,
          ValidAudience = tokenConfiguration.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Secret))
        }
      );

    return serviceCollection;
  }

  internal static IServiceCollection ConfigureRouteConstraints(this IServiceCollection serviceCollection) {
    var types = _coreAssembly
      .GetTypes().Where(
        type => type is {
          IsClass: true,
          IsAbstract: false
        } && type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(RoutingConstraintAttribute))
      );

    foreach (var type in types) {
      var attribute = type.GetCustomAttribute<RoutingConstraintAttribute>();
      var constraintName = attribute!.Name;
      var constraintType = typeof(EntityIdRouteConstraint<>).MakeGenericType(type);

      serviceCollection.AddRouting(options => options.ConstraintMap.Add(constraintName, constraintType));
    }

    return serviceCollection;
  }

  internal static IServiceCollection ConfigureProviders(this IServiceCollection serviceCollection) {
    var serviceTypes = _applicationAssembly
      .GetTypes().Where(
        type => type.IsInterface &&
                type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ProviderAttribute))
      );

    foreach (var serviceType in serviceTypes) {
      var implementationType = _iocAssembly
        .GetTypes().FirstOrDefault(
          type => type.GetInterfaces().Contains(serviceType) &&
                  type is { IsClass: true, IsAbstract: false } &&
                  type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(ProviderAttribute))
        );

      if (implementationType is null) {
        continue;
      }

      serviceCollection.AddScoped(serviceType, implementationType);
    }

    return serviceCollection;
  }

  internal static IServiceCollection ConfigureRepositories(this IServiceCollection serviceCollection) {
    var serviceTypes = _coreAssembly
      .GetTypes().Where(
        type => type.IsInterface &&
                type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(RepositoryAttribute))
      );

    foreach (var serviceType in serviceTypes) {
      var implementationType = _iocAssembly
        .GetTypes().FirstOrDefault(
          type => type.GetInterfaces().Contains(serviceType) &&
                  type is { IsClass: true, IsAbstract: false } &&
                  type.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(RepositoryAttribute))
        );

      if (implementationType is null) {
        continue;
      }

      serviceCollection.AddScoped(serviceType, implementationType);
    }

    return serviceCollection;
  }
}