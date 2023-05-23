// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Attributes;
using Calliope.Core.Entities.Users;
using Calliope.Core.Schemas.Authentication;

namespace Calliope.Application.Abstractions.Providers;

/// <summary>
///   Interface for JsonWebTokenProvider.
/// </summary>
[Provider]
public interface IJsonWebTokenProvider {
  /// <summary>
  ///   Generates a new access token for the given user.
  /// </summary>
  /// <param name="userId">The user identifier.</param>
  /// <param name="username">The username.</param>
  /// <returns>The generated access token.</returns>
  AccessTokenSchema GenerateAccessToken(UserId userId, string username);

  /// <summary>
  ///   Generates a new refresh token for the given user.
  /// </summary>
  /// <param name="userId">The user identifier.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The generated refresh token.</returns>
  Task<RefreshTokenSchema> GenerateRefreshTokenAsync(UserId userId, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Revokes the refresh token for the given user.
  /// </summary>
  /// <param name="refreshToken">The refresh token.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Validates the access token for the given user.
  /// </summary>
  /// <param name="token">The token.</param>
  /// <returns>True if the token is valid, false otherwise.</returns>
  bool ValidateAccessToken(string token);

  /// <summary>
  ///   Validates the refresh token for the given user.
  /// </summary>
  /// <param name="token">The token.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if the token is valid, false otherwise.</returns>
  Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Gets the user identifier and username from the given access token.
  /// </summary>
  /// <param name="accessToken">The token.</param>
  /// <returns>The user identifier and username.</returns>
  (UserId? userId, string? username) GetUserIdAndUsernameFromAccessToken(string accessToken);
}