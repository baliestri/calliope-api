// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Attributes;

/// <summary>
///   Defines a constraint for a route.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RoutingConstraintAttribute : Attribute {
  /// <summary>
  ///   Initializes a new instance of the <see cref="RoutingConstraintAttribute" /> class.
  /// </summary>
  /// <param name="name">The name of the constraint.</param>
  public RoutingConstraintAttribute(string name)
    => Name = name;

  /// <summary>
  ///   Gets the name of the constraint.
  /// </summary>
  public string Name { get; }
}