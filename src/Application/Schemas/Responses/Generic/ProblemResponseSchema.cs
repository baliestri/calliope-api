// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Application.Schemas.Responses.Generic;

/// <summary>
///   Represents a problem response schema.
/// </summary>
/// <param name="Type">The problem type.</param>
/// <param name="Title">The problem title.</param>
/// <param name="Status">The problem status.</param>
/// <param name="Detail">The problem detail.</param>
/// <param name="Instance">The problem instance.</param>
/// <param name="TraceId">The problem trace id.</param>
/// <param name="Errors">The problem errors.</param>
public readonly record struct ProblemResponseSchema(
  string? Type,
  string? Title,
  int Status,
  string? Detail,
  string? Instance,
  string? TraceId,
  IDictionary<string, string[]>? Errors
);