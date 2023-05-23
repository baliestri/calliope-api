// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Contracts;
using OneOf;

namespace Calliope.Application.Adapters;

/// <summary>
///   Represents a response that can be an error or a valid response.
/// </summary>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public sealed class ErrorOr<TResponse> : OneOfBase<TResponse, IEnumerable<ErrorObject>> where TResponse : notnull {
  /// <summary>
  ///   Initializes a new instance of the <see cref="ErrorOr{TResponse}" /> class.
  /// </summary>
  /// <param name="input">The input.</param>
  public ErrorOr(OneOf<TResponse, IEnumerable<ErrorObject>> input) : base(input) { }

  /// <summary>
  ///   Converts a response to an error or a valid response.
  /// </summary>
  /// <param name="response">The response.</param>
  /// <returns>The error or a valid response.</returns>
  public static implicit operator ErrorOr<TResponse>(TResponse response) => new(response);

  /// <summary>
  ///   Converts a list of errors to an error or a valid response.
  /// </summary>
  /// <param name="errors">The errors.</param>
  /// <returns>The error or a valid response.</returns>
  public static implicit operator ErrorOr<TResponse>(List<ErrorObject> errors) => new(errors);

  /// <summary>
  ///   Converts an error to an error or a valid response.
  /// </summary>
  /// <param name="error">The error.</param>
  /// <returns>The error or a valid response.</returns>
  public static implicit operator ErrorOr<TResponse>(ErrorObject error) => new(new List<ErrorObject> { error });
}
