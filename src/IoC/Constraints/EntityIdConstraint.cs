// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.Globalization;
using System.Reflection;
using Calliope.Core.Attributes;
using Calliope.Core.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace Calliope.IoC.Constraints;

/// <summary>
///   Defines a route constraint for <see cref="EntityId{TId}" />.
/// </summary>
/// <typeparam name="TEntityId">The type of the entity id.</typeparam>
public sealed class EntityIdRouteConstraint<TEntityId> : IRouteConstraint where TEntityId : EntityId<TEntityId> {
  private readonly ILogger<EntityIdRouteConstraint<TEntityId>> _logger;

  /// <summary>
  ///   Initializes a new instance of the <see cref="EntityIdRouteConstraint{TEntityId}" /> class.
  /// </summary>
  /// <param name="logger">The logger.</param>
  public EntityIdRouteConstraint(ILogger<EntityIdRouteConstraint<TEntityId>> logger) => _logger = logger;

  /// <inheritdoc />
  public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection) {
    if (httpContext is null) {
      throw new ArgumentNullException(nameof(httpContext));
    }

    if (route is null) {
      throw new ArgumentNullException(nameof(route));
    }

    if (routeKey is null) {
      throw new ArgumentNullException(nameof(routeKey));
    }

    if (values is null) {
      throw new ArgumentNullException(nameof(values));
    }

    if (!values.TryGetValue(routeKey, out var routeValue)) {
      return false;
    }

    try {
      var constraintType = typeof(TEntityId)
        .Assembly
        .GetTypes()
        .SingleOrDefault(type => type.GetCustomAttribute(typeof(RoutingConstraintAttribute)) is not null);

      if (constraintType is null) {
        _logger.LogError("No type with {RouteConstraintAttribute} found", nameof(RoutingConstraintAttribute));
        return false;
      }

      var entityId = typeof(EntityId<>).MakeGenericType(constraintType);
      var tryParseMethod = entityId.GetMethod("TryParse", BindingFlags.Public | BindingFlags.Static);

      if (tryParseMethod is null) {
        _logger.LogError("No method with 'TryParse' found in {EntityId}", entityId);
        return false;
      }

      var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
      return (bool)(tryParseMethod.Invoke(null, new object[] { parameterValueString!, null! }) ?? false);
    }
    catch (Exception ex) {
      _logger.LogError(ex, "Error while trying to match route constraint");
      return false;
    }
  }
}