// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Security.Cryptography;
using System.Text;
using Calliope.Application.Abstractions.Providers;
using Calliope.Core.Attributes;
using Calliope.Core.Entities.Users;
using Konscious.Security.Cryptography;

namespace Calliope.IoC.Providers;

/// <summary>
///   Provider for password hash.
/// </summary>
[Provider]
public sealed class PasswordHashProvider : IPasswordHashProvider {
  /// <inheritdoc />
  public UserPassword Generate(string raw) {
    using var rng = RandomNumberGenerator.Create();
    var salt = new byte[16];

    rng.GetBytes(salt);

    var argon2Id = new Argon2id(Encoding.UTF8.GetBytes(raw)) {
      Salt = salt,
      DegreeOfParallelism = 8,
      Iterations = 4,
      MemorySize = 1024 * 1024
    };

    var hash = argon2Id.GetBytes(16);

    return new UserPassword(hash, salt);
  }

  /// <inheritdoc />
  public bool Verify(string raw, UserPassword userPassword) {
    var (hash, salt) = userPassword;

    var argon2Id = new Argon2id(Encoding.UTF8.GetBytes(raw)) {
      Salt = salt,
      DegreeOfParallelism = 8,
      Iterations = 4,
      MemorySize = 1024 * 1024
    };

    var hashToVerify = argon2Id.GetBytes(16);

    return hashToVerify.SequenceEqual(hash);
  }
}