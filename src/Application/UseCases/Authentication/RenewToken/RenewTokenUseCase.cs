// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using MediatR;

namespace Calliope.Application.UseCases.Authentication.RenewToken;

/// <summary>
///   Represents a renew token use case.
/// </summary>
/// <param name="AccessToken">The access token.</param>
/// <param name="RefreshToken">The refresh token.</param>
public readonly record struct RenewTokenUseCase(
  string AccessToken,
  string RefreshToken
) : IRequest<ErrorOr<SignInResponseSchema>>;