// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.WebAPI.Attributes;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Calliope.WebAPI.Filters.OpenApi;

/// <summary>
///   Filter to ignore parameters in swagger.
/// </summary>
public sealed class ParameterIgnoreFilter : IOperationFilter {
  /// <inheritdoc />
  public void Apply(OpenApiOperation? operation, OperationFilterContext? context) {
    if (operation is null ||
        context?.ApiDescription?.ParameterDescriptions is null) {
      return;
    }

    var parametersToHide = context
      .ApiDescription
      .ParameterDescriptions
      .Where(parameterHasIgnoreAttribute)
      .ToList();

    if (!parametersToHide.Any()) {
      return;
    }

    var parameters = parametersToHide
      .Select(
        parameterToHide => operation.Parameters
          .FirstOrDefault(parameter => parameter.Name == parameterToHide.Name)
      );

    foreach (var parameter in parameters) {
      operation.Parameters.Remove(parameter);
    }
  }

  private static bool parameterHasIgnoreAttribute(ApiParameterDescription parameterDescription) {
    if (parameterDescription.ModelMetadata is DefaultModelMetadata metadata) {
      return metadata.Attributes.ParameterAttributes is not null &&
             metadata.Attributes.ParameterAttributes.Any(attribute => attribute is ParameterIgnoreAttribute);
    }

    return false;
  }
}