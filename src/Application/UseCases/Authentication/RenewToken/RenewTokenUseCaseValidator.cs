// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace Calliope.Application.UseCases.Authentication.RenewToken;

/// <summary>
///   Represents a <see cref="RenewTokenUseCase" /> validator.
/// </summary>
public sealed class RenewTokenUseCaseValidator : AbstractValidator<RenewTokenUseCase> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="RenewTokenUseCaseValidator" /> class.
  /// </summary>
  public RenewTokenUseCaseValidator() {
    RuleFor(renewToken => renewToken.AccessToken)
      .NotEmpty();

    RuleFor(renewToken => renewToken.RefreshToken)
      .NotEmpty();
  }
}