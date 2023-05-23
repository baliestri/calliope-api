// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Requests.Authentication;

/// <summary>
///   Represents a sign out request.
/// </summary>
/// <param name="RefreshToken">The refresh token.</param>
public readonly record struct SignOutRequestSchema(string RefreshToken);