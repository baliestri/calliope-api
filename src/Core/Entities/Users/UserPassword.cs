// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Exceptions.Users;
using Calliope.Core.Extensions;

namespace Calliope.Core.Entities.Users;

/// <summary>
///   Represents a <see cref="User" /> password.
/// </summary>
/// <param name="PasswordHash">The password hash.</param>
/// <param name="PasswordSalt">The password salt.</param>
public readonly record struct UserPassword(byte[] PasswordHash, byte[] PasswordSalt) {
  public static UserPassword FromString(string userPassword)
    => userPassword;

  public static implicit operator string(UserPassword userPassword)
    => $"{userPassword.PasswordHash.ToBase64()}.{userPassword.PasswordSalt.ToBase64()}";

  public static implicit operator UserPassword(string userPassword) {
    var split = userPassword.Split('.');

    if (split.Length != 2) {
      throw UserValidationException.WithPassword();
    }

    var passwordHash = split[0].FromBase64();
    var passwordSalt = split[1].FromBase64();

    return new UserPassword(passwordHash, passwordSalt);
  }

  public override string ToString()
    => $"{PasswordHash.ToBase64()}.{PasswordSalt.ToBase64()}";
}