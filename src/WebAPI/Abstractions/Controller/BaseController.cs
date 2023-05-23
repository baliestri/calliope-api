// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Adapters;
using Calliope.Core.Contracts;
using Calliope.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Calliope.WebAPI.Abstractions.Controller;

/// <summary>
///   Base class for all controllers.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase {
  /// <summary>
  ///   Handles the <see cref="ErrorOr{TResponse}" /> result of an operation.
  /// </summary>
  /// <param name="errors">The error list.</param>
  /// <returns>The compatible <see cref="IActionResult" />.</returns>
  protected IActionResult HandleProblem(IEnumerable<ErrorObject> errors) {
    var errorResponses = errors.ToList();
    if (errorResponses.All(error => error.Type is ErrorType.UnprocessableContent)) {
      return validationProblem(errorResponses);
    }

    HttpContext.Items["errors"] = errors;

    return problem(errorResponses.FirstOrDefault());
  }

  /// <summary>
  ///   Handles the <see cref="ErrorOr{TResponse}" /> result of an operation.
  /// </summary>
  /// <param name="error">The error.</param>
  /// <returns>The compatible <see cref="IActionResult" />.</returns>
  protected IActionResult HandleProblem(ErrorObject error) {
    if (error.Type is ErrorType.UnprocessableContent) {
      return validationProblem(new List<ErrorObject> { error });
    }

    HttpContext.Items["errors"] = new List<ErrorObject> { error };

    return problem(error);
  }

  private IActionResult problem(ErrorObject error) {
    var statusCode = error.Type switch {
      ErrorType.BadRequest => StatusCodes.Status400BadRequest,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Forbidden => StatusCodes.Status403Forbidden,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
      ErrorType.MethodNotAllowed => StatusCodes.Status405MethodNotAllowed,
      ErrorType.NotAcceptable => StatusCodes.Status406NotAcceptable,
      ErrorType.PaymentRequired => StatusCodes.Status402PaymentRequired,
      var _ => StatusCodes.Status500InternalServerError
    };

    return Problem(statusCode: statusCode, title: error.Code, detail: error.Message);
  }

  private IActionResult validationProblem(IEnumerable<ErrorObject> errors) {
    var modelStateDictionary = new ModelStateDictionary();

    foreach (var error in errors) {
      modelStateDictionary.AddModelError(error.Code, error.Message);
    }

    return ValidationProblem(statusCode: 422, modelStateDictionary: modelStateDictionary);
  }
}