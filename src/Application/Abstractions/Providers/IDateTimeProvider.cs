// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Attributes;

namespace Calliope.Application.Abstractions.Providers;

/// <summary>
///   Interface for DateTimeProvider.
/// </summary>
[Provider]
public interface IDateTimeProvider {
  /// <summary>
  ///   Gets the current date and time in UTC format.
  /// </summary>
  DateTime UtcNow { get; }
}