// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Linq.Expressions;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.SeedWork;
using Calliope.IoC.Abstractions.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Calliope.IoC.SeedWork;

/// <summary>
///   Strongly typed repository implementation.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
/// <typeparam name="TId">The entity id type.</typeparam>
public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId> where TId : EntityId<TId> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="Repository{TEntity,TId}" /> class.
  /// </summary>
  /// <param name="context">The injected <see cref="IDatabaseContext" />.</param>
  protected Repository(IDatabaseContext context)
    => Context = context;

  /// <summary>
  ///   Gets the database context.
  /// </summary>
  protected IDatabaseContext Context { get; }

  #region Disposable

  /// <inheritdoc />
  public void Dispose() {
    Context.Dispose();
    GC.SuppressFinalize(this);
  }

  #endregion

  #region Operations

  /// <inheritdoc />
  public void Add(TEntity entity)
    => Context.Set<TEntity>().Add(entity);

  /// <inheritdoc />
  public void AddRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().AddRange(entities);

  /// <inheritdoc />
  public void Update(TEntity entity)
    => Context.Set<TEntity>().Update(entity);

  /// <inheritdoc />
  public void UpdateRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().UpdateRange(entities);

  /// <inheritdoc />
  public void Delete(TEntity entity)
    => Context.Set<TEntity>().Remove(entity);

  /// <inheritdoc />
  public void DeleteRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().RemoveRange(entities);

  /// <inheritdoc />
  public void DeleteById(TId id) {
    var entity = Context.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(id));

    if (entity is not null) {
      Context.Set<TEntity>().Remove(entity);
    }
  }

  /// <inheritdoc />
  public void DeleteByIdRange(IEnumerable<TId> ids) {
    var entities = Context.Set<TEntity>().Where(entity => ids.Contains(entity.Id));

    if (entities.Any()) {
      Context.Set<TEntity>().RemoveRange(entities);
    }
  }

  #endregion

  #region Count

  /// <inheritdoc />
  public async Task<int> CountAllAsync(CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().CountAsync(entity => !entity.DeletedAt.HasValue, cancellationToken);

  /// <inheritdoc />
  public async Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).CountAsync(predicate, cancellationToken);

  #endregion

  #region Exists

  /// <inheritdoc />
  public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).AnyAsync(predicate, cancellationToken);

  /// <inheritdoc />
  public async Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).AnyAsync(entity => entity.Id.Equals(id), cancellationToken);

  #endregion

  #region Find

  /// <inheritdoc />
  public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).FirstOrDefaultAsync(predicate, cancellationToken);

  /// <inheritdoc />
  public async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);

  /// <inheritdoc />
  public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).ToListAsync(cancellationToken);

  /// <inheritdoc />
  public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).Where(predicate).ToListAsync(cancellationToken);

  #endregion
}

/// <summary>
///   Generic repository implementation.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity {
  /// <summary>
  ///   Initializes a new instance of the <see cref="Repository{TEntity}" /> class.
  /// </summary>
  /// <param name="context">The database context.</param>
  protected Repository(IDatabaseContext context)
    => Context = context;

  /// <summary>
  ///   Gets the database context.
  /// </summary>
  protected IDatabaseContext Context { get; }

  #region Disposable

  /// <inheritdoc />
  public void Dispose() {
    Context.Dispose();
    GC.SuppressFinalize(this);
  }

  #endregion

  #region Operations

  /// <inheritdoc />
  public void Add(TEntity entity)
    => Context.Set<TEntity>().Add(entity);

  /// <inheritdoc />
  public void AddRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().AddRange(entities);

  /// <inheritdoc />
  public void Update(TEntity entity)
    => Context.Set<TEntity>().Update(entity);

  /// <inheritdoc />
  public void UpdateRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().UpdateRange(entities);

  /// <inheritdoc />
  public void Delete(TEntity entity)
    => Context.Set<TEntity>().Remove(entity);

  /// <inheritdoc />
  public void DeleteRange(IEnumerable<TEntity> entities)
    => Context.Set<TEntity>().RemoveRange(entities);

  /// <inheritdoc />
  public void DeleteById(Guid id) {
    var entity = Context.Set<TEntity>().FirstOrDefault(entity => entity.Id.Equals(id));

    if (entity is not null) {
      Context.Set<TEntity>().Remove(entity);
    }
  }

  /// <inheritdoc />
  public void DeleteByIdRange(IEnumerable<Guid> ids) {
    var entities = Context.Set<TEntity>().Where(entity => ids.Contains(entity.Id));

    if (entities.Any()) {
      Context.Set<TEntity>().RemoveRange(entities);
    }
  }

  #endregion

  #region Count

  /// <inheritdoc />
  public async Task<int> CountAllAsync(CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().CountAsync(entity => !entity.DeletedAt.HasValue, cancellationToken);

  /// <inheritdoc />
  public async Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).CountAsync(predicate, cancellationToken);

  #endregion

  #region Exists

  /// <inheritdoc />
  public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).AnyAsync(predicate, cancellationToken);

  /// <inheritdoc />
  public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).AnyAsync(entity => entity.Id.Equals(id), cancellationToken);

  #endregion

  #region Find

  /// <inheritdoc />
  public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).FirstOrDefaultAsync(predicate, cancellationToken);

  /// <inheritdoc />
  public async Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).FirstOrDefaultAsync(entity => entity.Id.Equals(id), cancellationToken);

  /// <inheritdoc />
  public async Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).ToListAsync(cancellationToken);

  /// <inheritdoc />
  public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    => await Context.Set<TEntity>().Where(entity => !entity.DeletedAt.HasValue).Where(predicate).ToListAsync(cancellationToken);

  #endregion
}