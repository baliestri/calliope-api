// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;
using Calliope.Core.Exceptions.Users;

namespace Calliope.Core.Tests.Entities;

public sealed class UserTests {
  private readonly User _user;

  public UserTests() {
    var password = new UserPassword();
    _user = new User("Bruno", "Sales", "baliestri", "me@baliestri", password);
  }

  [Fact]
  public void ShouldUpdateInformation() {
    _user.WithInfo("John", "Doe", "jdoe", "jdoe@gmail.com");

    Assert.Equal("John", _user.FirstName);
    Assert.Equal("Doe", _user.LastName);
    Assert.Equal("jdoe", _user.Username);
    Assert.Equal("jdoe@gmail.com", _user.Email);
  }

  [Theory]
  [InlineData(null)]
  [InlineData("J")]
  [InlineData("JoeJoeJoeJoeJoeJoeJoe")]
  public void ShouldNotUpdateInformationWithInvalidFirstName(string firstName)
    => Assert.Throws<UserValidationException>(() => _user.WithInfo(firstName, "Doe", "jdoe", "jdoe@gmail.com"));

  [Theory]
  [InlineData(null)]
  [InlineData("D")]
  [InlineData("DoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoeDoe")]
  public void ShouldNotUpdateInformationWithInvalidLastName(string lastName)
    => Assert.Throws<UserValidationException>(() => _user.WithInfo("John", lastName, "jdoe", "jdoe@gmail.com"));

  [Theory]
  [InlineData(null)]
  [InlineData("j")]
  [InlineData("jdoejdoejdoejdoejdoe")]
  public void ShouldNotUpdateInformationWithInvalidUsername(string username)
    => Assert.Throws<UserValidationException>(() => _user.WithInfo("John", "Doe", username, "jdoe@gmail.com"));

  [Theory]
  [InlineData(null)]
  public void ShouldNotUpdateInformationWIthInvalidEmail(string email)
    => Assert.Throws<UserValidationException>(() => _user.WithInfo("John", "Doe", "jdoe", email));

  [Fact]
  public void ShouldUpdateRefreshToken() {
    var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(2);
    _user.WithRefreshToken("refreshToken", refreshTokenExpiration);

    Assert.Equal("refreshToken", _user.RefreshToken);
    Assert.Equal(refreshTokenExpiration, _user.RefreshTokenExpiration);
  }

  [Fact]
  public void ShouldNotUpdateRefreshToken()
    => Assert.Throws<UserValidationException>(() => _user.WithRefreshToken("refreshToken", DateTime.UtcNow.AddMinutes(-2)));
}