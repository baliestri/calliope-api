// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Extensions;

/// <summary>
///   Extensions for <see cref="string" />.
/// </summary>
public static class StringExtensions {
  /// <summary>
  ///   Transforms a string to snake case.
  /// </summary>
  /// <param name="value">The string to be transformed.</param>
  /// <returns>The string in snake case.</returns>
  public static string ToSnakeCase(this string value)
    => string.Concat(value.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();

  /// <summary>
  ///   Checks if a string is null or empty.
  /// </summary>
  /// <param name="value">The string to be checked.</param>
  /// <returns>True if the string is null or empty, false otherwise.</returns>
  public static bool IsNullOrEmpty(this string value)
    => string.IsNullOrEmpty(value);

  /// <summary>
  ///   Checks if a string is greater than a length.
  /// </summary>
  /// <param name="value">The string to be checked.</param>
  /// <param name="length">The length to be compared.</param>
  /// <returns>True if the string is greater than the length, false otherwise.</returns>
  public static bool IsGreaterThan(this string value, int length)
    => value.Length > length;

  /// <summary>
  ///   Checks if a string is less than a length.
  /// </summary>
  /// <param name="value">The string to be checked.</param>
  /// <param name="length">The length to be compared.</param>
  /// <returns>True if the string is less than the length, false otherwise.</returns>
  public static bool IsLessThan(this string value, int length)
    => value.Length < length;

  /// <summary>
  ///   Converts a string to a base64 byte array.
  /// </summary>
  /// <param name="value">The string.</param>
  /// <returns>The base64 byte array.</returns>
  public static byte[] FromBase64(this string value)
    => Convert.FromBase64String(value);
}