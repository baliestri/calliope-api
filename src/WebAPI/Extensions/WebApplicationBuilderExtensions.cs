// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.WebAPI.Extensions;

/// <summary>
///   Extensions for <see cref="WebApplicationBuilder" />.
/// </summary>
internal static class WebApplicationBuilderExtensions {
  internal static WebApplication RegisterComponents(this WebApplicationBuilder builder) {
    var configuration = builder.Configuration;
    var serviceCollection = builder.Services;

    serviceCollection
      .ConfigureProblemDetailsFactory()
      .ConfigureLogger()
      .ConfigureSwagger(configuration)
      .ConfigureJsonSerializers()
      .ConfigureRouting()
      .ConfigureLayers(configuration);

    return builder.Build();
  }
}