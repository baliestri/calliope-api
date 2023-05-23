// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Extensions;

/// <summary>
///   Extensions for <see cref="IEnumerable{T}" />
/// </summary>
public static class EnumerableExtensions {
  /// <summary>
  ///   ForEach extension for <see cref="IEnumerable{T}" />
  /// </summary>
  /// <param name="source">The source.</param>
  /// <param name="action">The action.</param>
  /// <typeparam name="T">The type.</typeparam>
  public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
    foreach (var item in source) {
      action(item);
    }
  }

  /// <summary>
  ///   Check if <see cref="IEnumerable{T}" /> is null or empty
  /// </summary>
  /// <param name="source">The source.</param>
  /// <typeparam name="T">The type.</typeparam>
  /// <returns>True if is null or empty, false otherwise.</returns>
  public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    => source is null || !source.Any();
}