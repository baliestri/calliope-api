// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace Calliope.Application.UseCases.Authentication.SignOut;

/// <summary>
///   Represents a <see cref="SignOutUseCase" /> validator.
/// </summary>
public sealed class SignOutUseCaseValidator : AbstractValidator<SignOutUseCase> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="SignOutUseCaseValidator" /> class.
  /// </summary>
  public SignOutUseCaseValidator()
    => RuleFor(signOut => signOut.RefreshToken)
      .NotEmpty();
}