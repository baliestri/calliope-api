// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Abstractions.Providers;
using Calliope.Application.Adapters;
using Calliope.Application.Schemas.Responses.Authentication;
using Calliope.Core.Abstractions.Repositories;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Calliope.Application.UseCases.Authentication.Register;

/// <summary>
///   Represents a <see cref="RegisterUseCase" /> handler.
/// </summary>
public sealed class RegisterUseCaseHandler : IRequestHandler<RegisterUseCase, ErrorOr<RegisterResponseSchema>> {
  private readonly IPasswordHashProvider _hashProvider;
  private readonly ILogger<RegisterUseCaseHandler> _logger;
  private readonly IUserRepository _repository;
  private readonly IJsonWebTokenProvider _tokenProvider;
  private readonly IUnitOfWork _unitOfWork;

  /// <summary>
  ///   Initializes a new instance of the <see cref="RegisterUseCaseHandler" /> class.
  /// </summary>
  /// <param name="logger">The injected <see cref="ILogger{TCategoryName}" />.</param>
  /// <param name="repository">The injected <see cref="IUserRepository" />.</param>
  /// <param name="unitOfWork">The injected <see cref="IUnitOfWork" />.</param>
  /// <param name="tokenProvider">The injected <see cref="IJsonWebTokenProvider" />.</param>
  /// <param name="hashProvider">The injected <see cref="IPasswordHashProvider" />.</param>
  public RegisterUseCaseHandler(
    IPasswordHashProvider hashProvider, ILogger<RegisterUseCaseHandler> logger, IUnitOfWork unitOfWork, IUserRepository repository,
    IJsonWebTokenProvider tokenProvider
  ) {
    _hashProvider = hashProvider;
    _logger = logger;
    _unitOfWork = unitOfWork;
    _repository = repository;
    _tokenProvider = tokenProvider;
  }

  /// <inheritdoc />
  public async Task<ErrorOr<RegisterResponseSchema>> Handle(RegisterUseCase request, CancellationToken cancellationToken) {
    var (firstName, lastName, username, email, password) = request;

    if (await _repository.ExistsByEmailAsync(email, cancellationToken)) {
      return AuthErrorResponses.WhenEmailAlreadyInUse;
    }

    if (await _repository.ExistsByUsernameAsync(username, cancellationToken)) {
      return AuthErrorResponses.WhenUsernameAlreadyInUse;
    }

    try {
      await _unitOfWork.BeginAsync(cancellationToken);

      var userPassword = _hashProvider.Generate(password);
      var user = new User(firstName, lastName, username, email, userPassword);

      _repository.Add(user);
      await _unitOfWork.CommitAsync(cancellationToken);
      return new RegisterResponseSchema(user.Id);
    }
    catch (Exception ex) {
      _logger.LogError(ex, "An error occurred while registering a new user with email {Email}", email);
      await _unitOfWork.RollbackAsync(cancellationToken);
      return AuthErrorResponses.WhenCouldNotRegister;
    }
  }
}