// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Entities.Users;

namespace Calliope.Core.Exceptions.Generic;

/// <summary>
///   Represents an exception thrown when a <see cref="User" /> entity is invalid.
/// </summary>
public abstract class EntityValidationException<TEntity> : Exception where TEntity : class {
  /// <summary>
  ///   Initializes a new instance of the <see cref="EntityValidationException{TEntity}" /> class.
  /// </summary>
  /// <param name="message">The exception message.</param>
  /// <param name="paramName">The parameter name.</param>
  protected EntityValidationException(string message, string? paramName = null) :
    base($"[{typeof(TEntity).Name}] {message} ({paramName})") { }
}