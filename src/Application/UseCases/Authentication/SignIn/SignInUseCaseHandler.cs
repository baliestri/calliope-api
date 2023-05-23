// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Abstractions.Providers;
using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using Calliope.Core.Abstractions.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calliope.Application.UseCases.Authentication.SignIn;

/// <summary>
///   Represents the <see cref="SignInUseCase" /> handler.
/// </summary>
public sealed class SignInUseCaseHandler : IRequestHandler<SignInUseCase, ErrorOr<SignInResponseSchema>> {
  private readonly IPasswordHashProvider _hashProvider;
  private readonly ILogger<SignInUseCaseHandler> _logger;
  private readonly IUserRepository _repository;
  private readonly IJsonWebTokenProvider _tokenProvider;

  /// <summary>
  ///   Initializes a new instance of the <see cref="SignInUseCaseHandler" /> class.
  /// </summary>
  /// <param name="hashProvider">The injected <see cref="IPasswordHashProvider" />.</param>
  /// <param name="logger">The injected <see cref="ILogger{TCategoryName}" />.</param>
  /// <param name="repository">The injected <see cref="IUserRepository" />.</param>
  /// <param name="tokenProvider">The injected <see cref="IJsonWebTokenProvider" />.</param>
  public SignInUseCaseHandler(
    IPasswordHashProvider hashProvider, ILogger<SignInUseCaseHandler> logger, IUserRepository repository, IJsonWebTokenProvider tokenProvider
  ) {
    _hashProvider = hashProvider;
    _logger = logger;
    _repository = repository;
    _tokenProvider = tokenProvider;
  }

  /// <inheritdoc />
  public async Task<ErrorOr<SignInResponseSchema>> Handle(SignInUseCase request, CancellationToken cancellationToken) {
    var (usernameOrEmail, password) = request;
    var user = await _repository.FindByUsernameOrEmailAsync(usernameOrEmail, cancellationToken);

    if (user is null) {
      return AuthErrorResponses.WhenInvalidCredentials;
    }

    try {
      if (!_hashProvider.Verify(password, user.Password)) {
        return AuthErrorResponses.WhenInvalidCredentials;
      }

      var accessToken = _tokenProvider.GenerateAccessToken(user.Id, user.Username);
      var refreshToken = await _tokenProvider.GenerateRefreshTokenAsync(user.Id, cancellationToken);

      return new SignInResponseSchema(user.Id, accessToken, refreshToken);
    }
    catch (Exception ex) {
      _logger.LogError(ex, "An error occurred while trying to sign in the user {UserId}", user.Id);
      return AuthErrorResponses.WhenCouldNotSignIn;
    }
  }
}