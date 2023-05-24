// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;

namespace Calliope.Core.Exceptions.Users;

/// <summary>
///   Exception thrown when a user is not found.
/// </summary>
public sealed class UserNotFoundException : Exception {
  private UserNotFoundException(string message) : base(message) { }

  /// <summary>
  ///   Creates a new <see cref="UserNotFoundException" /> with the given user id.
  /// </summary>
  /// <param name="userId">The user id.</param>
  /// <returns>A new <see cref="UserNotFoundException" />.</returns>
  public static UserNotFoundException WithUserId(UserId userId) => new($"The provided user id {userId} was not found.");

  /// <summary>
  ///   Creates a new <see cref="UserNotFoundException" /> with the given username or email.
  /// </summary>
  /// <param name="usernameOrEmail">The username or email.</param>
  /// <returns>A new <see cref="UserNotFoundException" />.</returns>
  public static UserNotFoundException WithUsernameOrEmail(string usernameOrEmail) => new($"The provided username or email {usernameOrEmail} was not found.");

  /// <summary>
  ///   Creates a new <see cref="UserNotFoundException" /> with the given refresh token.
  /// </summary>
  /// <param name="refreshToken">The refresh token.</param>
  /// <returns>A new <see cref="UserNotFoundException" />.</returns>
  public static UserNotFoundException WithRefreshToken(string refreshToken) => new($"The provided refresh token {refreshToken} was not found.");
}