// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Attributes;
using Calliope.Core.Entities.Users;

namespace Calliope.Core.Abstractions.Repositories;

/// <summary>
///   Interface for <see cref="User" /> repository.
/// </summary>
[Repository]
public interface IUserRepository : IRepository<User, UserId> {
  /// <summary>
  ///   Checks if an user exists by email.
  /// </summary>
  /// <param name="email">The email to search.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if exists, otherwise null.</returns>
  Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Checks if an user exists by username.
  /// </summary>
  /// <param name="username">The username to search.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if exists, otherwise null.</returns>
  Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds an user by username or email.
  /// </summary>
  /// <param name="usernameOrEmail">The username or email to search.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The user if found, otherwise null.</returns>
  Task<User?> FindByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds an user by refresh token.
  /// </summary>
  /// <param name="refreshToken">The refresh token to search.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns></returns>
  Task<User?> FindByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
}