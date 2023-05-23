// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Contracts;
using FluentValidation;
using MediatR;

namespace Calliope.Application.Behaviors;

/// <summary>
///   A behavior that validates the request before sending it to the handler.
/// </summary>
/// <typeparam name="TRequest">The type of the request.</typeparam>
/// <typeparam name="TResponse">The type of the response.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  where TRequest : IRequest<TResponse>
  where TResponse : notnull {
  private readonly IValidator<TRequest>? _validator;

  /// <summary>
  ///   Initializes a new instance of the <see cref="ValidationBehavior{TRequest,TResponse}" /> class.
  /// </summary>
  /// <param name="validator">The validator.</param>
  public ValidationBehavior(IValidator<TRequest>? validator = null)
    => _validator = validator;

  /// <inheritdoc />
  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
    if (_validator is null) {
      return await next();
    }

    var result = await _validator.ValidateAsync(request, cancellationToken);

    if (result.IsValid) {
      return await next();
    }

    var errorResponses = result.Errors.ConvertAll(failure => ErrorObject.UnprocessableContent(failure.PropertyName, failure.ErrorMessage));

    return (dynamic)errorResponses;
  }
}
