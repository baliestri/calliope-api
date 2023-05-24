// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Abstractions.Repositories;
using Calliope.Core.Attributes;
using Calliope.Core.Entities.Users;
using Calliope.IoC.Abstractions.SeedWork;
using Calliope.IoC.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Calliope.IoC.Repositories;

/// <summary>
///   Repository for <see cref="User" />.
/// </summary>
[Repository]
public sealed class UserRepository : Repository<User, UserId>, IUserRepository {
  /// <summary>
  ///   Initializes a new instance of the <see cref="UserRepository" /> class.
  /// </summary>
  /// <param name="context">The injected <see cref="IDatabaseContext" />.</param>
  public UserRepository(IDatabaseContext context) : base(context) { }

  /// <inheritdoc />
  public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    => await Context.Users
      .Where(user => !user.DeletedAt.HasValue)
      .AnyAsync(user => user.Email == email, cancellationToken);

  /// <inheritdoc />
  public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    => await Context.Users
      .Where(user => !user.DeletedAt.HasValue)
      .AnyAsync(user => user.Username == username, cancellationToken);

  /// <inheritdoc />
  public async Task<User?> FindByUsernameOrEmailAsync(string usernameOrEmail, CancellationToken cancellationToken = default)
    => await Context.Users
      .Where(user => !user.DeletedAt.HasValue)
      .FirstOrDefaultAsync(user => user.Username == usernameOrEmail || user.Email == usernameOrEmail, cancellationToken);

  /// <inheritdoc />
  public async Task<User?> FindByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
    => await Context.Users
      .Where(user => !user.DeletedAt.HasValue)
      .FirstOrDefaultAsync(user => user.RefreshToken == refreshToken, cancellationToken);
}