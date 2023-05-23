// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Core.Contracts;

namespace Calliope.Application.UseCases.Authentication;

/// <summary>
///   Authentication use cases error responses.
/// </summary>
public static class AuthErrorResponses {
  /// <summary>
  ///   Error response for when the email is already in use.
  /// </summary>
  public static ErrorObject WhenEmailAlreadyInUse
    => ErrorObject.Conflict("ERR_EMAIL_ALREADY_IN_USE", "The email is already in use.");

  /// <summary>
  ///   Error response for when the username is already in use.
  /// </summary>
  public static ErrorObject WhenUsernameAlreadyInUse
    => ErrorObject.Conflict("ERR_USERNAME_ALREADY_IN_USE", "The username is already in use.");

  /// <summary>
  ///   Error response for when the provided credentials are invalid.
  /// </summary>
  public static ErrorObject WhenInvalidCredentials
    => ErrorObject.Unauthorized("ERR_INVALID_CREDENTIALS", "The provided credentials are invalid.");

  /// <summary>
  ///   Error response for when the user could not be registered.
  /// </summary>
  public static ErrorObject WhenCouldNotRegister
    => ErrorObject.InternalServerError("ERR_COULD_NOT_REGISTER", "An error occurred while registering the user.");

  /// <summary>
  ///   Error response for when the user could not be signed in.
  /// </summary>
  public static ErrorObject WhenCouldNotSignIn
    => ErrorObject.InternalServerError("ERR_COULD_NOT_SIGN_IN", "An error occurred while signing in the user.");

  /// <summary>
  ///   Error response for when the refresh token is invalid.
  /// </summary>
  public static ErrorObject WhenRefreshTokenIsInvalid
    => ErrorObject.Unauthorized("ERR_REFRESH_TOKEN_IS_INVALID", "The provided refresh token is invalid.");

  /// <summary>
  ///   Error response for when the access token is invalid.
  /// </summary>
  public static ErrorObject WhenAccessTokenIsInvalid
    => ErrorObject.Unauthorized("ERR_ACCESS_TOKEN_IS_INVALID", "The provided access token is invalid.");

  /// <summary>
  ///   Error response for when the tokens could not be renewed.
  /// </summary>
  public static ErrorObject WhenCouldNotRenewTokens
    => ErrorObject.InternalServerError("ERR_COULD_NOT_RENEW_TOKENS", "An error occurred while renewing the tokens.");

  /// <summary>
  ///   Error response for when the tokens could not be revoked.
  /// </summary>
  public static ErrorObject WhenCouldNotRevokeTokens
    => ErrorObject.InternalServerError("ERR_COULD_NOT_REVOKE_TOKENS", "An error occurred while revoking the tokens.");
}