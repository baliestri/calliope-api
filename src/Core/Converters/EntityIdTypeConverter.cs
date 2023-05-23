// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Calliope.Core.SeedWork;
using Calliope.Core.Utilities;

namespace Calliope.Core.Converters;

public sealed class EntityIdTypeConverter<TId> : TypeConverter where TId : EntityId<TId> {
  private static readonly TypeConverter _idValueConverter = getIdValueConverter();

  private readonly Type _type;

  /// <summary>
  ///   Initializes a new instance of the <see cref="EntityIdTypeConverter{TEntityId}" /> class.
  /// </summary>
  /// <param name="type">The type of the entity id.</param>
  public EntityIdTypeConverter(Type type)
    => _type = type;

  private static TypeConverter getIdValueConverter() {
    var converter = TypeDescriptor.GetConverter(typeof(Guid));
    if (!converter.CanConvertFrom(typeof(string))) {
      throw new InvalidOperationException(
        $"Type '{typeof(Guid)}' doesn't have a converter that can convert from string"
      );
    }

    return converter;
  }

  /// <inheritdoc />
  public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    => sourceType == typeof(string) ||
       sourceType == typeof(Guid) ||
       base.CanConvertFrom(context, sourceType);

  /// <inheritdoc />
  public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
    => destinationType == typeof(string) ||
       destinationType == typeof(Guid) ||
       base.CanConvertTo(context, destinationType);

  /// <inheritdoc />
  public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value) {
    if (value is string stringValue) {
      value = _idValueConverter.ConvertFrom(stringValue)!;
    }

    if (value is Guid idValue) {
      var factory = EntityIdUtilities.GetFactory(_type);
      return factory(idValue);
    }

    return base.ConvertFrom(context, culture, value);
  }

  /// <inheritdoc />
  public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType) {
    if (value is null) {
      throw new ArgumentNullException(nameof(value));
    }

    var entityId = (EntityId<TId>)value;
    var idValue = entityId.Value;

    if (destinationType == typeof(string)) {
      return idValue.ToString();
    }

    return destinationType == typeof(Guid)
      ? idValue
      : base.ConvertTo(context, culture, value, destinationType);
  }
}