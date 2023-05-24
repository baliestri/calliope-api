// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using MediatR;

namespace Calliope.Application.UseCases.Authentication.SignIn;

/// <summary>
///   Represents a sign in use case.
/// </summary>
/// <param name="EmailOrUsername">The user email or username.</param>
/// <param name="Password">The user password.</param>
public sealed record SignInUseCase(
  string EmailOrUsername,
  string Password
) : IRequest<ErrorOr<SignInResponseSchema>>;
