// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Text.Json;
using System.Text.Json.Serialization;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Utilities;

namespace Calliope.Core.Converters;

/// <summary>
///   Json converter for <see cref="IEntityId{TId}" />.
/// </summary>
/// <typeparam name="TEntityId">The type of the entity identifier.</typeparam>
public sealed class EntityIdJsonConverter<TEntityId> : JsonConverter<TEntityId> where TEntityId : IEntityId<TEntityId> {
  /// <inheritdoc />
  public override TEntityId? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
    if (reader.TokenType is JsonTokenType.Null) {
      return default;
    }

    var value = JsonSerializer.Deserialize<Guid>(ref reader, options);
    var factory = EntityIdUtilities.GetFactory(typeToConvert);
    return (TEntityId?)factory(value);
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, TEntityId? value, JsonSerializerOptions options) {
    if (value is null) {
      writer.WriteNullValue();

      return;
    }

    JsonSerializer.Serialize(writer, value.Value, options);
  }
}