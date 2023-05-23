// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Abstractions.SeedWork;

/// <summary>
///   Interface for strongly typed entity identifiers.
/// </summary>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
public interface IEntityId<TId> : IEquatable<Guid>, IComparable<Guid> where TId : IEntityId<TId> {
  /// <summary>
  ///   Gets the value of the entity identifier.
  /// </summary>
  Guid Value { get; }

  /// <summary>
  ///   Creates a new entity identifier with a given value.
  /// </summary>
  /// <param name="value">The value of the entity identifier.</param>
  /// <returns>The entity identifier.</returns>
  static abstract TId FromGuid(Guid value);

  /// <summary>
  ///   Creates a new entity identifier with a given value.
  /// </summary>
  /// <param name="value">The value of the entity identifier.</param>
  /// <returns>The entity identifier.</returns>
  static abstract TId FromString(string value);

  /// <summary>
  ///   Creates a new entity identifier with a new value.
  /// </summary>
  /// <returns>The entity identifier.</returns>
  static abstract TId New();

  /// <summary>
  ///   Tries to parse a string into an entity identifier.
  /// </summary>
  /// <param name="value">The string value to parse.</param>
  /// <param name="id">The entity identifier.</param>
  /// <returns>True if the string was parsed successfully, false otherwise.</returns>
  static abstract bool TryParse(string value, out TId id);
}