// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Requests.Authentication;

/// <summary>
///   Represents a sign in request.
/// </summary>
/// <param name="EmailOrUsername">The user email or username.</param>
/// <param name="Password">The user password.</param>
public readonly record struct SignInRequestSchema(
  string EmailOrUsername,
  string Password
);