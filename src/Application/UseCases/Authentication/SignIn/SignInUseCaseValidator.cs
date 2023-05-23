// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace Calliope.Application.UseCases.Authentication.SignIn;

/// <summary>
///   Represents a <see cref="SignInUseCase" /> validator.
/// </summary>
public sealed class SignInUseCaseValidator : AbstractValidator<SignInUseCase> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="SignInUseCaseValidator" /> class.
  /// </summary>
  public SignInUseCaseValidator() {
    RuleFor(signIn => signIn.EmailOrUsername)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(200);

    RuleFor(signIn => signIn.Password)
      .NotEmpty()
      .MinimumLength(12)
      .MaximumLength(200)
      .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])");
  }
}