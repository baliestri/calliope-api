// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Linq.Expressions;

namespace Calliope.Core.Abstractions.SeedWork;

/// <summary>
///   Interface for strongly typed repositories.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IRepository<TEntity, in TId> : IDisposable where TEntity : IEntity<TId> where TId : IEntityId<TId> {
  #region Operations

  /// <summary>
  ///   Adds an entity to the repository.
  /// </summary>
  /// <param name="entity">The entity to add.</param>
  void Add(TEntity entity);

  /// <summary>
  ///   Adds a range of entities to the repository.
  /// </summary>
  /// <param name="entities">The entities to add.</param>
  void AddRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Updates an entity in the repository.
  /// </summary>
  /// <param name="entity">The entity to update.</param>
  void Update(TEntity entity);

  /// <summary>
  ///   Updates a range of entities in the repository.
  /// </summary>
  /// <param name="entities">The entities to update.</param>
  void UpdateRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Deletes an entity from the repository.
  /// </summary>
  /// <param name="entity">The entity to delete.</param>
  void Delete(TEntity entity);

  /// <summary>
  ///   Deletes a range of entities from the repository.
  /// </summary>
  /// <param name="entities">The entities to delete.</param>
  void DeleteRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Deletes an entity from the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to delete.</param>
  void DeleteById(TId id);

  /// <summary>
  ///   Deletes a range of entities from the repository by their identifiers.
  /// </summary>
  /// <param name="ids">The identifiers of the entities to delete.</param>
  void DeleteByIdRange(IEnumerable<TId> ids);

  #endregion

  #region Count

  /// <summary>
  ///   Counts all entities in the repository.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The number of entities.</returns>
  Task<int> CountAllAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Counts all entities in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The number of entities.</returns>
  Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  #endregion

  #region Exists

  /// <summary>
  ///   Checks if an entity exists in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if the entity exists, false otherwise.</returns>
  Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Checks if an entity exists in the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to find.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if the entity exists, false otherwise.</returns>
  Task<bool> ExistsByIdAsync(TId id, CancellationToken cancellationToken = default);

  #endregion

  #region Find

  /// <summary>
  ///   Finds an entity in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entity if found, null otherwise.</returns>
  Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds an entity in the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to find.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entity if found, null otherwise.</returns>
  Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds all entities in the repository.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entities.</returns>
  Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds all entities in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entities.</returns>
  Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  #endregion
}

/// <summary>
///   Interface for generic repositories.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface IRepository<TEntity> : IDisposable where TEntity : IEntity {
  #region Operations

  /// <summary>
  ///   Adds an entity to the repository.
  /// </summary>
  /// <param name="entity">The entity to add.</param>
  void Add(TEntity entity);

  /// <summary>
  ///   Adds a range of entities to the repository.
  /// </summary>
  /// <param name="entities">The entities to add.</param>
  void AddRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Updates an entity in the repository.
  /// </summary>
  /// <param name="entity">The entity to update.</param>
  void Update(TEntity entity);

  /// <summary>
  ///   Updates a range of entities in the repository.
  /// </summary>
  /// <param name="entities">The entities to update.</param>
  void UpdateRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Deletes an entity from the repository.
  /// </summary>
  /// <param name="entity">The entity to delete.</param>
  void Delete(TEntity entity);

  /// <summary>
  ///   Deletes a range of entities from the repository.
  /// </summary>
  /// <param name="entities">The entities to delete.</param>
  void DeleteRange(IEnumerable<TEntity> entities);

  /// <summary>
  ///   Deletes an entity from the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to delete.</param>
  void DeleteById(Guid id);

  /// <summary>
  ///   Deletes a range of entities from the repository by their identifiers.
  /// </summary>
  /// <param name="ids">The identifiers of the entities to delete.</param>
  void DeleteByIdRange(IEnumerable<Guid> ids);

  #endregion

  #region Count

  /// <summary>
  ///   Counts all entities in the repository.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The number of entities.</returns>
  Task<int> CountAllAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Counts all entities in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The number of entities.</returns>
  Task<int> CountAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  #endregion

  #region Exists

  /// <summary>
  ///   Checks if an entity exists in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if the entity exists, false otherwise.</returns>
  Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Checks if an entity exists in the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to find.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>True if the entity exists, false otherwise.</returns>
  Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);

  #endregion

  #region Find

  /// <summary>
  ///   Finds an entity in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entity if found, null otherwise.</returns>
  Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds an entity in the repository by its identifier.
  /// </summary>
  /// <param name="id">The identifier of the entity to find.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entity if found, null otherwise.</returns>
  Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds all entities in the repository.
  /// </summary>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entities.</returns>
  Task<IEnumerable<TEntity>> FindAllAsync(CancellationToken cancellationToken = default);

  /// <summary>
  ///   Finds all entities in the repository by a predicate.
  /// </summary>
  /// <param name="predicate">The predicate to use.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The entities.</returns>
  Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

  #endregion
}