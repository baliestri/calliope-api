// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Enums;

namespace Calliope.Core.Contracts;

/// <summary>
///   Represents an error object that will be converted to a response.
/// </summary>
public readonly record struct ErrorObject {
  private ErrorObject(string code, string message, ErrorType type) {
    Code = code;
    Message = message;
    Type = type;
    Status = (int)type;
  }

  /// <summary>
  ///   The error code.
  /// </summary>
  public string Code { get; }

  /// <summary>
  ///   The error message.
  /// </summary>
  public string Message { get; }

  /// <summary>
  ///   The error type.
  /// </summary>
  public ErrorType Type { get; }

  /// <summary>
  ///   The error status code.
  /// </summary>
  public int Status { get; }

  /// <summary>
  ///   Creates a new error response.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <param name="type">The error type.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject Create(string code, string message, ErrorType type)
    => new(code, message, type);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.BadRequest" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject BadRequest(string code, string message)
    => Create(code, message, ErrorType.BadRequest);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.Unauthorized" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject Unauthorized(string code, string message)
    => Create(code, message, ErrorType.Unauthorized);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.PaymentRequired" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject PaymentRequired(string code, string message)
    => Create(code, message, ErrorType.PaymentRequired);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.Forbidden" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject Forbidden(string code, string message)
    => Create(code, message, ErrorType.Forbidden);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.NotFound" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject NotFound(string code, string message)
    => Create(code, message, ErrorType.NotFound);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.NotAcceptable" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject NotAcceptable(string code, string message)
    => Create(code, message, ErrorType.NotAcceptable);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.Conflict" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject Conflict(string code, string message)
    => Create(code, message, ErrorType.Conflict);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.UnprocessableContent" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject UnprocessableContent(string code, string message)
    => Create(code, message, ErrorType.UnprocessableContent);

  /// <summary>
  ///   Creates a new error response with the <see cref="ErrorType.InternalServerError" /> type.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <returns>The error response.</returns>
  public static ErrorObject InternalServerError(string code, string message)
    => Create(code, message, ErrorType.InternalServerError);
}