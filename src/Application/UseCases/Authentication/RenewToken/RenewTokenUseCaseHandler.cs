// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Abstractions.Providers;
using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calliope.Application.UseCases.Authentication.RenewToken;

/// <summary>
///   Represents a <see cref="RenewTokenUseCase" /> handler.
/// </summary>
public sealed class RenewTokenUseCaseHandler : IRequestHandler<RenewTokenUseCase, ErrorOr<SignInResponseSchema>> {
  private readonly ILogger<RenewTokenUseCaseHandler> _logger;
  private readonly IJsonWebTokenProvider _tokenProvider;

  /// <summary>
  ///   Initializes a new instance of the <see cref="RenewTokenUseCaseHandler" /> class.
  /// </summary>
  /// <param name="logger">The injected <see cref="ILogger{TCategoryName}" />.</param>
  /// <param name="tokenProvider">The injected <see cref="IJsonWebTokenProvider" />.</param>
  public RenewTokenUseCaseHandler(ILogger<RenewTokenUseCaseHandler> logger, IJsonWebTokenProvider tokenProvider) {
    _logger = logger;
    _tokenProvider = tokenProvider;
  }

  /// <inheritdoc />
  public async Task<ErrorOr<SignInResponseSchema>> Handle(RenewTokenUseCase request, CancellationToken cancellationToken) {
    var (accessToken, refreshToken) = request;
    var isValid = await _tokenProvider.ValidateRefreshTokenAsync(refreshToken, cancellationToken);

    if (!isValid) {
      return AuthErrorResponses.WhenRefreshTokenIsInvalid;
    }

    var (userId, username) = _tokenProvider.GetUserIdAndUsernameFromAccessToken(accessToken);

    if (userId is null ||
        username is null) {
      return AuthErrorResponses.WhenAccessTokenIsInvalid;
    }

    try {
      var newAccessToken = _tokenProvider.GenerateAccessToken(userId, username);
      var newRefreshToken = await _tokenProvider.GenerateRefreshTokenAsync(userId, cancellationToken);

      return new SignInResponseSchema(userId, newAccessToken, newRefreshToken);
    }
    catch (Exception exception) {
      _logger.LogError(exception, "An error occurred while renewing tokens for user {UserId}", userId);
      return AuthErrorResponses.WhenCouldNotRenewTokens;
    }
  }
}