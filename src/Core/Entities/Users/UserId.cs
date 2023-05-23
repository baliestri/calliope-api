// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Attributes;
using Calliope.Core.SeedWork;

namespace Calliope.Core.Entities.Users;

/// <summary>
///   Strongly typed Id for <see cref="User" />.
/// </summary>
/// <param name="Value">The value of the Id.</param>
[RoutingConstraint("userId")]
public sealed record UserId(Guid Value) : EntityId<UserId>(Value);