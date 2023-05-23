// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.ComponentModel;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Converters;

namespace Calliope.Core.SeedWork;

/// <summary>
///   Base class for strongly typed entity identifiers.
/// </summary>
/// <param name="Value">The value of the entity identifier.</param>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
[TypeConverter(typeof(EntityIdTypeConverter<>))]
public record EntityId<TId>(Guid Value) : IEntityId<TId> where TId : IEntityId<TId> {
  /// <inheritdoc />
  public virtual bool Equals(Guid other)
    => Value.Equals(other);

  /// <inheritdoc />
  public int CompareTo(Guid other)
    => Value.CompareTo(other);

  /// <inheritdoc />
  public static TId FromGuid(Guid value) {
    try {
      var instance = Activator.CreateInstance(typeof(TId), value);

      if (instance is not TId id) {
        throw new InvalidOperationException($"The type {typeof(TId)} is not a valid entity identifier.");
      }

      return id;
    }
    catch (MissingMethodException) {
      throw new InvalidOperationException($"The type {typeof(TId)} does not have a constructor that accepts a Guid.");
    }
    catch (Exception ex) {
      throw new InvalidOperationException($"An error occurred while creating a new instance of {typeof(TId)}.", ex);
    }
  }

  /// <inheritdoc />
  public static TId FromString(string value) {
    if (!Guid.TryParse(value, out var guid)) {
      throw new FormatException($"The value {value} is not a valid Guid.");
    }

    return FromGuid(guid);
  }

  /// <inheritdoc />
  public static TId New()
    => FromGuid(Guid.NewGuid());

  /// <inheritdoc />
  public static bool TryParse(string value, out TId id) {
    if (Guid.TryParse(value, out var guid)) {
      id = FromGuid(guid);
      return true;
    }

    id = default!;
    return false;
  }

  public static bool operator ==(Guid? left, EntityId<TId>? right)
    => left is not null && right is not null && left.Equals(right.Value);

  public static bool operator !=(Guid? left, EntityId<TId>? right)
    => left is null || right is null || !left.Equals(right.Value);

  public static implicit operator string(EntityId<TId> id)
    => id.Value.ToString();

  public static implicit operator Guid(EntityId<TId> id)
    => id.Value;
}