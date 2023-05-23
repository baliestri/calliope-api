// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Requests.Authentication;

/// <summary>
///   Represents an register request.
/// </summary>
/// <param name="FirstName">The user first name.</param>
/// <param name="LastName">The user last name.</param>
/// <param name="Username">The user username.</param>
/// <param name="Email">The user email.</param>
/// <param name="Password">The user password.</param>
public readonly record struct RegisterRequestSchema(
  string FirstName,
  string LastName,
  string Username,
  string Email,
  string Password
);