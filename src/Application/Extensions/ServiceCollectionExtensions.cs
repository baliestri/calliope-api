// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Reflection;
using Calliope.Application.Behaviors;
using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Calliope.Application.Extensions;

/// <summary>
///   Extensions for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions {
  /// <summary>
  ///   This method is responsible for the Application layer of the application.
  /// </summary>
  /// <param name="serviceCollection">The service collection.</param>
  /// <returns>The service collection.</returns>
  public static IServiceCollection AddApplicationLayer(this IServiceCollection serviceCollection)
    => serviceCollection
      .ConfigureMediatR()
      .ConfigureValidators()
      .ConfigureMappings();

  internal static IServiceCollection ConfigureMediatR(this IServiceCollection serviceCollection)
    => serviceCollection
      .AddMediatR(
        configuration => {
          configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
          configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        }
      );

  internal static IServiceCollection ConfigureValidators(this IServiceCollection serviceCollection)
    => serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

  internal static IServiceCollection ConfigureMappings(this IServiceCollection serviceCollection) {
    var adapterConfig = TypeAdapterConfig.GlobalSettings;
    adapterConfig.Scan(typeof(ServiceCollectionExtensions).Assembly);

    return serviceCollection
      .AddSingleton(adapterConfig)
      .AddScoped<IMapper, ServiceMapper>();
  }
}