// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Responses.Authentication;

/// <summary>
///   Represents a register response.
/// </summary>
/// <param name="UserId">The user identifier.</param>
public readonly record struct RegisterResponseSchema(
  Guid UserId
);