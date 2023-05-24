// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;
using Calliope.Core.Exceptions.Generic;

namespace Calliope.Core.Exceptions.Users;

/// <summary>
///   Exception thrown when a user validation fails.
/// </summary>
public sealed class UserValidationException : EntityValidationException<User> {
  private UserValidationException(string message, string? paramName = null) : base(message, paramName) { }

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <param name="firstName">The first name of the user.</param>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithFirstName(string firstName)
    => new($"The first name '{firstName}' is invalid.", nameof(firstName));

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <param name="lastName">The last name of the user.</param>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithLastName(string lastName)
    => new($"The last name '{lastName}' is invalid.", nameof(lastName));

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <param name="username">The username of the user.</param>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithUsername(string username)
    => new($"The username '{username}' is invalid.", nameof(username));

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <param name="email">The email of the user.</param>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithEmail(string email)
    => new($"The email '{email}' is invalid.", nameof(email));

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <param name="refreshTokenExpiration">The refresh token expiration of the user.</param>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithRefreshTokenExpiration(DateTime? refreshTokenExpiration)
    => new($"The refresh token '{refreshTokenExpiration}' is invalid.", nameof(refreshTokenExpiration));

  /// <summary>
  ///   Creates a new <see cref="UserValidationException" /> with the specified message.
  /// </summary>
  /// <returns>A new <see cref="UserValidationException" /> with the specified message.</returns>
  public static UserValidationException WithPassword()
    => new("The password object is invalid.", nameof(UserPassword));
}