// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Calliope.IoC.Abstractions.SeedWork;

/// <summary>
///   Interface for database context.
/// </summary>
public interface IDatabaseContext : IDisposable {
  #region Entities

  DbSet<User> Users { get; set; }

  #endregion

  /// <summary>
  ///   Provides access to database related information and operations for this context.
  /// </summary>
  DatabaseFacade Database { get; }

  /// <summary>
  ///   Creates a <see cref="DbSet{TEntity}" /> that can be used to query and save instances of TEntity
  /// </summary>
  /// <typeparam name="TEntity">The type of entity for which a set should be returned.</typeparam>
  /// <returns>A set for the given entity type.</returns>
  DbSet<TEntity> Set<TEntity>() where TEntity : class;

  /// <summary>
  ///   Saves all changes made in this context to the database
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>
  ///   A task that represents the asynchronous save operation. The task result contains the number of state entries
  ///   written to the database.
  /// </returns>
  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}