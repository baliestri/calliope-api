// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Attributes;
using Calliope.Core.Entities.Users;

namespace Calliope.Application.Abstractions.Providers;

/// <summary>
///   Interface for PasswordHashProvider.
/// </summary>
[Provider]
public interface IPasswordHashProvider {
  /// <summary>
  ///   Generates a new password hash for the given password.
  /// </summary>
  /// <param name="raw">The raw password.</param>
  /// <returns>The generated password hash.</returns>
  UserPassword Generate(string raw);

  /// <summary>
  ///   Verifies if the given raw password matches the given password hash.
  /// </summary>
  /// <param name="raw">The raw password.</param>
  /// <param name="userPassword">The password hash.</param>
  /// <returns>True if the password matches, false otherwise.</returns>
  bool Verify(string raw, UserPassword userPassword);
}