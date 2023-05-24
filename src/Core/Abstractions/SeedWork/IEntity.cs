// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Abstractions.SeedWork;

/// <summary>
///   Interface for strongly typed entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IEntity<TId> : IComparable<TId>, IEquatable<TId> where TId : IEntityId<TId> {
  /// <summary>
  ///   Gets the entity identifier.
  /// </summary>
  TId Id { get; }

  /// <summary>
  ///   Gets the date and time when the entity was created.
  /// </summary>
  DateTimeOffset CreatedAt { get; }

  /// <summary>
  ///   Gets the date and time when the entity was last updated.
  /// </summary>
  DateTimeOffset? UpdatedAt { get; }

  /// <summary>
  ///   Gets the date and time when the entity was deleted logically.
  /// </summary>
  DateTimeOffset? DeletedAt { get; }
}

/// <summary>
///   Interface for generic entities.
/// </summary>
public interface IEntity : IComparable<Guid>, IEquatable<Guid> {
  /// <summary>
  ///   Gets the entity identifier.
  /// </summary>
  Guid Id { get; }

  /// <summary>
  ///   Gets the date and time when the entity was created.
  /// </summary>
  DateTimeOffset CreatedAt { get; }

  /// <summary>
  ///   Gets the date and time when the entity was last updated.
  /// </summary>
  DateTimeOffset? UpdatedAt { get; }

  /// <summary>
  ///   Gets the date and time when the entity was deleted logically.
  /// </summary>
  DateTimeOffset? DeletedAt { get; }
}