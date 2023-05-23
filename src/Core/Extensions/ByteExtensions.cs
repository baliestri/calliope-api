// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Extensions;

/// <summary>
///   Extensions for <see cref="byte" />.
/// </summary>
public static class ByteExtensions {
  /// <summary>
  ///   Converts a base64 byte array to a string.
  /// </summary>
  /// <param name="value">The base64 byte array.</param>
  /// <returns>The string.</returns>
  public static string ToBase64(this byte[] value)
    => Convert.ToBase64String(value);
}