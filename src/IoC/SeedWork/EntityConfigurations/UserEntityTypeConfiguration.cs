// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calliope.IoC.SeedWork.EntityConfigurations;

public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder.ToTable("users");

    builder
      .HasKey(user => user.Id);
    builder
      .Property(user => user.Id)
      .HasConversion(userId => userId.Value, guid => UserId.FromGuid(guid));

    builder
      .Property(user => user.FirstName)
      .HasMaxLength(20)
      .IsRequired();

    builder
      .Property(user => user.LastName)
      .HasMaxLength(30)
      .IsRequired();

    builder
      .Property(user => user.Username)
      .HasMaxLength(15)
      .IsRequired();

    builder
      .Property(user => user.Email)
      .IsRequired();

    builder
      .Property(user => user.Password)
      .HasConversion(password => password.ToString(), value => UserPassword.FromString(value));

    builder
      .HasIndex(user => user.Username)
      .IsUnique();

    builder
      .HasIndex(user => user.Email)
      .IsUnique();

    builder
      .HasIndex(user => user.RefreshToken)
      .IsUnique();
  }
}