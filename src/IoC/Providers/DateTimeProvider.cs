// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Abstractions.Providers;
using Calliope.Core.Attributes;

namespace Calliope.IoC.Providers;

/// <summary>
///   Provider for date time.
/// </summary>
[Provider]
public sealed class DateTimeProvider : IDateTimeProvider {
  /// <inheritdoc />
  public DateTime UtcNow
    => DateTime.UtcNow;
}