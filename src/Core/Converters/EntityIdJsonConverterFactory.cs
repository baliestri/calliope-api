// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;
using Calliope.Core.SeedWork;
using Calliope.Core.Utilities;

namespace Calliope.Core.Converters;

/// <summary>
///   Json converter factory for <see cref="EntityId{TId}" />.
/// </summary>
public sealed class EntityIdJsonConverterFactory : JsonConverterFactory {
  private static readonly ConcurrentDictionary<Type, JsonConverter> _cache = new();

  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert)
    => EntityIdUtilities.IsEntityId(typeToConvert);

  /// <inheritdoc />
  public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    => _cache.GetOrAdd(typeToConvert, createConverter);

  private static JsonConverter createConverter(Type typeToConvert) {
    if (!EntityIdUtilities.IsEntityId(typeToConvert)) {
      throw new InvalidOperationException($"Cannot create converter for '{typeToConvert}'");
    }

    var type = typeof(EntityIdJsonConverter<>).MakeGenericType(typeToConvert);
    return (JsonConverter)Activator.CreateInstance(type)!;
  }
}