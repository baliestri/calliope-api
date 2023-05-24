// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Schemas.Authentication;

namespace Calliope.Application.Schemas.Responses.Authentication;

/// <summary>
///   Represents a sign in response.
/// </summary>
/// <param name="UserId">The user identifier.</param>
/// <param name="AccessToken">The access token.</param>
/// <param name="RefreshTokenSchema">The refresh token.</param>
public readonly record struct SignInResponseSchema(
  Guid UserId,
  AccessTokenSchema AccessToken,
  RefreshTokenSchema RefreshToken
);