// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using MediatR;

namespace Calliope.Application.UseCases.Authentication.Register;

/// <summary>
///   Represents a register use case.
/// </summary>
/// <param name="FirstName">The first name of the user.</param>
/// <param name="LastName">The last name of the user.</param>
/// <param name="Username">The username of the user.</param>
/// <param name="Email">The email of the user.</param>
/// <param name="Password">The password of the user.</param>
public sealed record RegisterUseCase(
  string FirstName,
  string LastName,
  string Username,
  string Email,
  string Password
) : IRequest<ErrorOr<RegisterResponseSchema>>;
