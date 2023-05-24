// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Abstractions.Providers;
using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Generic;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calliope.Application.UseCases.Authentication.SignOut;

/// <summary>
///   Represents a <see cref="SignOutUseCase" /> handler.
/// </summary>
public sealed class SignOutUseCaseHandler : IRequestHandler<SignOutUseCase, ErrorOr<MessageResponseSchema>> {
  private readonly ILogger<SignOutUseCaseHandler> _logger;
  private readonly IJsonWebTokenProvider _tokenProvider;

  /// <summary>
  ///   Initializes a new instance of the <see cref="SignOutUseCaseHandler" /> class.
  /// </summary>
  /// <param name="logger">The injected <see cref="ILogger{TCategoryName}" />.</param>
  /// <param name="tokenProvider">The injected <see cref="IJsonWebTokenProvider" />.</param>
  public SignOutUseCaseHandler(ILogger<SignOutUseCaseHandler> logger, IJsonWebTokenProvider tokenProvider) {
    _logger = logger;
    _tokenProvider = tokenProvider;
  }

  /// <inheritdoc />
  public async Task<ErrorOr<MessageResponseSchema>> Handle(SignOutUseCase request, CancellationToken cancellationToken) {
    var refreshToken = request.RefreshToken;

    var isValid = await _tokenProvider.ValidateRefreshTokenAsync(refreshToken, cancellationToken);

    if (!isValid) {
      return AuthErrorResponses.WhenRefreshTokenIsInvalid;
    }

    try {
      await _tokenProvider.RevokeRefreshTokenAsync(refreshToken, cancellationToken);

      return new MessageResponseSchema("User successfully signed out.");
    }
    catch (Exception exception) {
      _logger.LogError(exception, "An error occurred while revoking refresh token {RefreshToken}", refreshToken);
      return AuthErrorResponses.WhenCouldNotRevokeTokens;
    }
  }
}