// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Schemas.Authentication;

/// <summary>
///   Refresh Token contract for Json Web Token.
/// </summary>
/// <param name="Value">The refresh token.</param>
/// <param name="ExpiresIn">The expiration date.</param>
public readonly record struct RefreshTokenSchema(string Value, DateTime ExpiresIn);