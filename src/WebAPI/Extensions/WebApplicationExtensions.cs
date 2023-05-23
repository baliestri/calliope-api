// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Calliope.WebAPI.Extensions;

/// <summary>
///   Extensions for <see cref="WebApplication" />.
/// </summary>
public static class WebApplicationExtensions {
  internal static WebApplication RegisterPipelines(this WebApplication application) {
    var configuration = application.Configuration;
    var serviceProvider = application.Services;

    application
      .UseExceptionHandler()
      .UseSwaggerForDevelopment()
      .UseHttpsRedirection()
      .UseRouting()
      .UseAuthentication()
      .UseAuthorization()
      .UseEndpoints(builder => builder.MapControllers());

    return application;
  }

  internal static WebApplication UseExceptionHandler(this WebApplication application) {
    var isDevelopment = application.Environment.IsDevelopment();

    application
      .UseExceptionHandler("/api/error")
      .UseHsts();

    application
      .Map(
        "/api/error", (HttpContext httpContext) => {
          var context = httpContext.Features.Get<IExceptionHandlerFeature>();
          var exception = context?.Error;
          var extensions = new Dictionary<string, object?>();

          if (exception is not null) {
            extensions.Add("innerException", exception.InnerException?.Message);
          }

          extensions.Add("traceId", Activity.Current?.Id ?? httpContext.TraceIdentifier);

          return isDevelopment
            ? Results.Problem(
              exception?.Message,
              statusCode: 500,
              title: "An error occurred while processing your request.",
              instance: context?.Path,
              extensions: extensions
            )
            : Results.Problem(title: "An error occurred while processing your request.", statusCode: 500);
        }
      );

    return application;
  }

  internal static WebApplication UseSwaggerForDevelopment(this WebApplication application) {
    var isDevelopment = application.Environment.IsDevelopment();

    if (isDevelopment) {
      application.UseSwagger();

      using var scope = application.Services.CreateScope();
      var provider = scope.ServiceProvider.GetService<IApiVersionDescriptionProvider>();

      application.UseSwaggerUI(
        options => provider?.ApiVersionDescriptions.ToList().ForEach(
          description => options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant()
          )
        )
      );
    }

    return application;
  }
}