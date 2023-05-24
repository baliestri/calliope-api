// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.SeedWork;

namespace Calliope.Core.Tests.Mocks;

public sealed record MockEntityId(Guid Value) : EntityId<MockEntityId>(Value);