// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Configurations;

/// <summary>
///   Swagger configuration.
/// </summary>
public sealed class SwaggerConfiguration {
  /// <summary>
  ///   The title of the API.
  /// </summary>
  public string Title { get; init; } = null!;

  /// <summary>
  ///   The description of the API.
  /// </summary>
  public string Description { get; init; } = null!;

  /// <summary>
  ///   The information about the contact of the API.
  /// </summary>
  public SwaggerContactConfiguration Contact { get; init; }

  /// <summary>
  ///   The information about the license of the API.
  /// </summary>
  public SwaggerLicenseConfiguration License { get; init; }

  /// <summary>
  ///   The versions of the API.
  /// </summary>
  public IEnumerable<string> Versions { get; init; } = Enumerable.Empty<string>();
}

/// <summary>
///   Swagger contact configuration.
/// </summary>
/// <param name="Name">The name of the contact person/organization.</param>
/// <param name="Url">The URL pointing to the contact information.</param>
/// <param name="Email">The email address of the contact person/organization.</param>
public readonly record struct SwaggerContactConfiguration(
  string Name,
  string Url,
  string Email
);

/// <summary>
///   Swagger license configuration.
/// </summary>
/// <param name="Name">The license name used for the API.</param>
/// <param name="Url">A URL to the license used for the API.</param>
public readonly record struct SwaggerLicenseConfiguration(
  string Name,
  string Url
);