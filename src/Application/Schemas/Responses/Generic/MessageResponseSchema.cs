// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Responses.Generic;

/// <summary>
///   Represents a generic message response.
/// </summary>
/// <param name="Message">The message.</param>
public readonly record struct MessageResponseSchema(string Message);