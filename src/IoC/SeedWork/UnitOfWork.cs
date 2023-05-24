// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Abstractions.SeedWork;
using Calliope.IoC.Abstractions.SeedWork;
using Microsoft.EntityFrameworkCore.Storage;

namespace Calliope.IoC.SeedWork;

/// <summary>
///   Implementation of the unit of work pattern.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork {
  private readonly IDatabaseContext _context;
  private IDbContextTransaction? _transaction;

  /// <summary>
  ///   Initializes a new instance of the <see cref="UnitOfWork" /> class.
  /// </summary>
  /// <param name="context">The injected <see cref="IDatabaseContext" />.</param>
  public UnitOfWork(IDatabaseContext context)
    => _context = context;

  #region Disposable

  /// <inheritdoc />
  public void Dispose() {
    _context.Dispose();
    _transaction?.Dispose();
  }

  #endregion

  /// <inheritdoc />
  public async Task BeginAsync(CancellationToken cancellationToken = default)
    => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

  /// <inheritdoc />
  public async Task CommitAsync(CancellationToken cancellationToken = default) {
    if (_transaction is null) {
      throw new InvalidOperationException("The transaction has not been started.");
    }

    await _context.SaveChangesAsync(cancellationToken);
    await _transaction.CommitAsync(cancellationToken);
  }

  /// <inheritdoc />
  public async Task RollbackAsync(CancellationToken cancellationToken = default) {
    if (_transaction is null) {
      throw new InvalidOperationException("The transaction has not been started.");
    }

    await _transaction.RollbackAsync(cancellationToken);
  }
}