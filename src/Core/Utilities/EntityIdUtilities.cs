// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Concurrent;
using System.Linq.Expressions;
using Calliope.Core.SeedWork;

namespace Calliope.Core.Utilities;

/// <summary>
///   Utilities for strongly-typed ids.
/// </summary>
public static class EntityIdUtilities {
  private static readonly ConcurrentDictionary<Type, Delegate> _factories = new();

  /// <summary>
  ///   Creates a new instance of the strongly-typed id.
  /// </summary>
  /// <param name="type">The type of the strongly-typed id.</param>
  /// <returns>A new instance of the strongly-typed id.</returns>
  public static Func<Guid, object> GetFactory(Type type)
    => (Func<Guid, object>)_factories.GetOrAdd(type, createFactory);

  private static Func<Guid, object> createFactory(Type type) {
    if (!IsEntityId(type)) {
      throw new ArgumentException(
        $"Type '{type}' is not a strongly-typed id type", nameof(type)
      );
    }

    var ctor = type.GetConstructor(new[] { typeof(Guid) });
    if (ctor is null) {
      throw new ArgumentNullException(
        nameof(type), $"Type '{type}' doesn't have a constructor with one parameter of type '{typeof(Guid)}'"
      );
    }

    var param = Expression.Parameter(typeof(Guid), "value");
    var body = Expression.New(ctor, param);
    var lambda = Expression.Lambda<Func<Guid, object>>(body, param);

    return lambda.Compile();
  }

  /// <summary>
  ///   Checks if the type is a strongly-typed id.
  /// </summary>
  /// <param name="typeToConvert">The type to check.</param>
  /// <returns>True if the type is a strongly-typed id, false otherwise.</returns>
  /// <exception cref="ArgumentNullException">If <paramref name="typeToConvert" /> is null.</exception>
  public static bool IsEntityId(Type typeToConvert) {
    if (typeToConvert is null) {
      throw new ArgumentNullException(nameof(typeToConvert));
    }

    return typeToConvert.BaseType is { IsGenericType: true } baseType &&
           baseType.GetGenericTypeDefinition() == typeof(EntityId<>);
  }
}