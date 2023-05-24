// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Configurations;

/// <summary>
///   Json Web Token configuration.
/// </summary>
public sealed class JsonWebTokenConfiguration {
  /// <summary>
  ///   The issuer of the token.
  /// </summary>
  public string Issuer { get; init; } = null!;

  /// <summary>
  ///   The audience of the token.
  /// </summary>
  public string Audience { get; init; } = null!;

  /// <summary>
  ///   The secret used to sign the token.
  /// </summary>
  public string Secret { get; init; } = null!;

  /// <summary>
  ///   The expiration time of the access token in minutes.
  /// </summary>
  public int AccessExpirationInMinutes { get; init; }

  /// <summary>
  ///   The expiration time of the refresh token in days.
  /// </summary>
  public int RefreshExpirationInDays { get; init; }
}