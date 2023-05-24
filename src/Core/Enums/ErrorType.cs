// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

namespace Calliope.Core.Enums;

/// <summary>
///   Error types based on HTTP Status Code.
/// </summary>
public enum ErrorType {
  /// <summary>
  ///   400 Bad Request.
  /// </summary>
  BadRequest = 400,

  /// <summary>
  ///   401 Unauthorized.
  /// </summary>
  Unauthorized = 401,

  /// <summary>
  ///   402 Payment Required.
  /// </summary>
  PaymentRequired = 402,

  /// <summary>
  ///   403 Forbidden.
  /// </summary>
  Forbidden = 403,

  /// <summary>
  ///   404 Not Found.
  /// </summary>
  NotFound = 404,

  /// <summary>
  ///   405 Method Not Allowed.
  /// </summary>
  MethodNotAllowed = 405,

  /// <summary>
  ///   406 Not Acceptable.
  /// </summary>
  NotAcceptable = 406,

  /// <summary>
  ///   409 Conflict.
  /// </summary>
  Conflict = 409,

  /// <summary>
  ///   422 Unprocessable Content.
  /// </summary>
  UnprocessableContent = 422,

  /// <summary>
  ///   500 Internal Server Error.
  /// </summary>
  InternalServerError = 500
}