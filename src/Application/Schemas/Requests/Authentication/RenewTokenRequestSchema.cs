// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Requests.Authentication;

/// <summary>
///   Represents a renew token request.
/// </summary>
/// <param name="AccessToken">The access token.</param>
/// <param name="RefreshToken">The refresh token.</param>
public readonly record struct RenewTokenRequestSchema(
  string AccessToken,
  string RefreshToken
);