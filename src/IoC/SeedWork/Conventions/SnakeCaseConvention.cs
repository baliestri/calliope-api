// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Calliope.IoC.SeedWork.Conventions;

/// <summary>
///   Convention for rename database objects to snake case.
/// </summary>
public sealed class SnakeCaseConvention : IModelFinalizingConvention {
  /// <inheritdoc />
  public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context) {
    var entityTypes = modelBuilder.Metadata.GetEntityTypes();

    foreach (var entityType in entityTypes) {
      entityType.SetTableName(entityType.GetTableName()?.ToSnakeCase());
      var tableName = entityType.GetTableName();

      entityType.SetSchema(entityType.GetSchema()?.ToLower().ToSnakeCase());
      var schemaName = entityType.GetSchema();

      entityType
        .GetProperties()
        .ForEach(x => x.SetColumnName(x.GetColumnName(StoreObjectIdentifier.Table(tableName!, schemaName))?.ToSnakeCase()));

      entityType
        .GetKeys()
        .ForEach(x => x.SetName(x.GetName()?.ToSnakeCase()));

      entityType
        .GetForeignKeys()
        .ForEach(x => x.SetConstraintName(x.GetConstraintName()?.ToSnakeCase()));

      entityType
        .GetIndexes()
        .ForEach(x => x.SetDatabaseName(x.GetDatabaseName()?.ToSnakeCase()));
    }
  }
}