// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Exceptions.Users;
using Calliope.Core.Extensions;
using Calliope.Core.SeedWork;

namespace Calliope.Core.Entities.Users;

/// <summary>
///   Represents a User entity.
/// </summary>
public sealed class User : Entity<UserId> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="User" /> class.
  /// </summary>
  /// <param name="id">The identifier of the entity.</param>
  /// <param name="firstName">The first name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="username">The unique username of the user.</param>
  /// <param name="email">The unique email of the user.</param>
  /// <param name="password">The password of the user.</param>
  public User(UserId id, string firstName, string lastName, string username, string email, UserPassword password) : base(id) {
    FirstName = firstName;
    LastName = lastName;
    Username = username;
    Email = email;
    Password = password;
  }

  /// <summary>
  ///   Initializes a new instance of the <see cref="User" /> class.
  /// </summary>
  /// <param name="firstName">The first name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="username">The unique username of the user.</param>
  /// <param name="email">The unique email of the user.</param>
  /// <param name="password">The password of the user.</param>
  public User(string firstName, string lastName, string username, string email, UserPassword password) : base(UserId.New()) {
    FirstName = firstName;
    LastName = lastName;
    Username = username;
    Email = email;
    Password = password;
  }

  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public string Username { get; private set; }
  public string Email { get; private set; }
  public UserPassword Password { get; private set; }
  public string? RefreshToken { get; private set; }
  public DateTime? RefreshTokenExpiration { get; private set; }

  /// <summary>
  ///   Updates the user's information.
  /// </summary>
  /// <param name="firstName">The first name of the user.</param>
  /// <param name="lastName">The last name of the user.</param>
  /// <param name="username">The unique username of the user.</param>
  /// <param name="email">The unique email of the user.</param>
  /// <exception cref="UserValidationException">Thrown when the user's information is invalid.</exception>
  public void WithInfo(string firstName, string lastName, string username, string email) {
    if (firstName.IsNullOrEmpty() ||
        firstName.IsGreaterThan(20) ||
        firstName.IsLessThan(2)) {
      throw UserValidationException.WithFirstName(firstName);
    }

    if (lastName.IsNullOrEmpty() ||
        lastName.IsGreaterThan(30) ||
        lastName.IsLessThan(2)) {
      throw UserValidationException.WithLastName(lastName);
    }

    if (username.IsNullOrEmpty() ||
        username.IsGreaterThan(15) ||
        username.IsLessThan(3)) {
      throw UserValidationException.WithUsername(username);
    }

    if (email.IsNullOrEmpty()) {
      throw UserValidationException.WithEmail(email);
    }

    FirstName = firstName;
    LastName = lastName;
    Username = username;
    Email = email;
    UpdatedAt = DateTimeOffset.UtcNow;
  }

  /// <summary>
  ///   Updates the user's password.
  /// </summary>
  /// <param name="passwordHash">The password hash.</param>
  /// <param name="passwordSalt">The password salt.</param>
  public void WithPassword(byte[] passwordHash, byte[] passwordSalt)
    => Password = new UserPassword(passwordHash, passwordSalt);

  /// <summary>
  ///   Updates the user's refresh token and its expiration.
  /// </summary>
  /// <param name="refreshToken">The refresh token.</param>
  /// <param name="refreshTokenExpiration">The refresh token expiration.</param>
  /// <exception cref="UserValidationException">Thrown when the refresh token or its expiration is invalid.</exception>
  public void WithRefreshToken(string? refreshToken, DateTime? refreshTokenExpiration) {
    if (refreshTokenExpiration == DateTime.MinValue ||
        refreshTokenExpiration == DateTime.MaxValue ||
        refreshTokenExpiration < DateTime.UtcNow) {
      throw UserValidationException.WithRefreshTokenExpiration(refreshTokenExpiration);
    }

    RefreshToken = refreshToken;
    RefreshTokenExpiration = refreshTokenExpiration;
    UpdatedAt = DateTimeOffset.UtcNow;
  }

  /// <summary>
  ///   Marks the user as deleted.
  /// </summary>
  public void MarkAsDeleted()
    => DeletedAt = DateTimeOffset.UtcNow;
}