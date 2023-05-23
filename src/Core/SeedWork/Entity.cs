// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Abstractions.SeedWork;

namespace Calliope.Core.SeedWork;

/// <summary>
///   Base class for strongly typed entities.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public abstract class Entity<TId> : IEntity<TId> where TId : IEntityId<TId> {
  /// <summary>
  ///   Initializes a new instance of the <see cref="Entity{TId}" /> class.
  /// </summary>
  /// <param name="entityId">The entity identifier.</param>
  protected Entity(TId entityId) {
    Id = entityId;
    CreatedAt = DateTimeOffset.UtcNow;
  }

  /// <summary>
  ///   Initializes a new instance of the <see cref="Entity{TId}" /> class.
  /// </summary>
  protected Entity() { }

  /// <inheritdoc />
  public TId Id { get; init; } = TId.New();

  /// <inheritdoc />
  public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

  /// <inheritdoc />
  public DateTimeOffset? UpdatedAt { get; protected set; }

  /// <inheritdoc />
  public DateTimeOffset? DeletedAt { get; protected set; }

  /// <inheritdoc />
  public int CompareTo(TId? other)
    => other is not null ? Id.Value.CompareTo(other.Value) : 1;

  /// <inheritdoc />
  public bool Equals(TId? other)
    => other is not null && Id.Value.Equals(other.Value);

  protected bool Equals(Entity<TId>? other)
    => other is not null && Id.Value.Equals(other.Id.Value);

  /// <inheritdoc />
  public override bool Equals(object? obj) {
    if (ReferenceEquals(null, obj)) {
      return false;
    }

    if (ReferenceEquals(this, obj)) {
      return true;
    }

    return obj.GetType() == GetType() && Equals((Entity<TId>)obj);
  }

  /// <inheritdoc />
  public override int GetHashCode()
    => Id.Value.GetHashCode();

  public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    => left is not null && left.Equals(right);

  public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    => left is null || !left.Equals(right);

  public static bool operator ==(Guid? left, Entity<TId>? right)
    => left is not null && right is not null && left.Equals(right.Id.Value);

  public static bool operator !=(Guid? left, Entity<TId>? right)
    => left is null || right is null || !left.Equals(right.Id.Value);
}

/// <summary>
///   Base class for generic entities.
/// </summary>
public abstract class Entity : IEntity {
  /// <summary>
  ///   Initializes a new instance of the <see cref="Entity" /> class.
  /// </summary>
  /// <param name="entityId">The entity identifier.</param>
  protected Entity(Guid entityId) {
    Id = entityId;
    CreatedAt = DateTimeOffset.UtcNow;
  }

  /// <summary>
  ///   Initializes a new instance of the <see cref="Entity" /> class.
  /// </summary>
  protected Entity() { }

  /// <inheritdoc />
  public int CompareTo(Guid other)
    => Id.CompareTo(other);

  /// <inheritdoc />
  public Guid Id { get; init; } = Guid.NewGuid();

  /// <inheritdoc />
  public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;

  /// <inheritdoc />
  public DateTimeOffset? UpdatedAt { get; protected set; }

  /// <inheritdoc />
  public DateTimeOffset? DeletedAt { get; protected set; }

  /// <inheritdoc />
  public bool Equals(Guid other)
    => Id.Equals(other);

  protected bool Equals(Entity other)
    => Id.Equals(other.Id);

  /// <inheritdoc />
  public override bool Equals(object? obj) {
    if (ReferenceEquals(null, obj)) {
      return false;
    }

    if (ReferenceEquals(this, obj)) {
      return true;
    }

    return obj.GetType() == GetType() && Equals((Entity)obj);
  }

  /// <inheritdoc />
  public override int GetHashCode()
    => Id.GetHashCode();

  public static bool operator ==(Entity? left, Entity? right)
    => left is not null && right is not null && left.Equals(right);

  public static bool operator !=(Entity? left, Entity? right)
    => left is null || right is null || !left.Equals(right);

  public static bool operator ==(Guid? left, Entity? right)
    => left is not null && right is not null && left.Equals(right.Id);

  public static bool operator !=(Guid? left, Entity? right)
    => left is null || right is null || !left.Equals(right.Id);
}