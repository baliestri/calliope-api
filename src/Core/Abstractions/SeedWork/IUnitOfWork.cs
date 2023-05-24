// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Abstractions.SeedWork;

/// <summary>
///   Interface for the unit of work pattern.
/// </summary>
public interface IUnitOfWork : IDisposable {
  #region Operations

  /// <summary>
  ///   Begins a new transaction.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task BeginAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Commits the transaction.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task CommitAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Rollbacks the transaction.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task RollbackAsync(CancellationToken cancellationToken = default);

  #endregion
}