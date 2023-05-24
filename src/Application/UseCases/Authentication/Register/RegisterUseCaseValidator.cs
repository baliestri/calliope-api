// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using FluentValidation;

namespace Calliope.Application.UseCases.Authentication.Register;

/// <summary>
///   Represents a <see cref="RegisterUseCase" /> validation rules.
/// </summary>
public sealed class RegisterUseCaseValidator : AbstractValidator<RegisterUseCase> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="RegisterUseCaseValidator" /> class.
  /// </summary>
  public RegisterUseCaseValidator() {
    RuleFor(register => register.FirstName)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(50);

    RuleFor(register => register.LastName)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(50);

    RuleFor(register => register.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(register => register.Username)
      .NotEmpty()
      .MinimumLength(3)
      .MaximumLength(15)
      .Matches(@"^[a-zA-Z0-9]+$");

    RuleFor(register => register.Password)
      .NotEmpty()
      .MinimumLength(12)
      .Matches(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])");
  }
}