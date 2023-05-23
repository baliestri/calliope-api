// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Tests.Mocks;

namespace Calliope.Core.Tests.SeedWork;

public sealed class EntityIdTests {
  [Fact]
  public void ShouldCreateEntityId() {
    var id = MockEntityId.New();

    Assert.NotNull(id);
    Assert.NotEqual(Guid.Empty, id.Value);
  }

  [Fact]
  public void ShouldCreateEntityIdFromGuid() {
    var guid = Guid.NewGuid();
    var id = MockEntityId.FromGuid(guid);

    Assert.NotNull(id);
    Assert.Equal(guid, id.Value);
  }

  [Fact]
  public void ShouldCreateEntityIdFromString() {
    var guid = Guid.NewGuid();
    var id = MockEntityId.FromString(guid.ToString());

    Assert.NotNull(id);
    Assert.Equal(guid, id.Value);
  }

  [Fact]
  public void ShouldNotCreateEntityIdFromInvalidGuid()
    => Assert.Throws<FormatException>(() => MockEntityId.FromGuid(Guid.Parse("invalid-guid")));

  [Fact]
  public void ShouldNotCreateEntityIdFromInvalidString()
    => Assert.Throws<FormatException>(() => MockEntityId.FromString("invalid-guid"));

  [Fact]
  public void ShouldParseEntityIdFromString() {
    var guid = Guid.NewGuid();

    Assert.True(MockEntityId.TryParse(guid.ToString(), out var id));
    Assert.NotNull(id);
    Assert.Equal(guid, id.Value);
  }

  [Fact]
  public void ShouldNotParseEntityIdFromString() {
    Assert.False(MockEntityId.TryParse("invalid-guid", out var id));
    Assert.Null(id);
  }
}