// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Calliope.Application.Abstractions.Providers;
using Calliope.Core.Abstractions.Repositories;
using Calliope.Core.Abstractions.SeedWork;
using Calliope.Core.Attributes;
using Calliope.Core.Configurations;
using Calliope.Core.Entities.Users;
using Calliope.Core.Exceptions.Users;
using Calliope.Core.Schemas.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Calliope.IoC.Providers;

/// <summary>
///   Provider for Json Web Tokens.
/// </summary>
[Provider]
public sealed class JsonWebTokenProvider : IJsonWebTokenProvider {
  private readonly JsonWebTokenConfiguration _configuration;
  private readonly IDateTimeProvider _dateTime;
  private readonly ILogger<JsonWebTokenProvider> _logger;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public JsonWebTokenProvider(
    IOptions<JsonWebTokenConfiguration> configuration, IDateTimeProvider dateTime, IUserRepository userRepository, IUnitOfWork unitOfWork,
    ILogger<JsonWebTokenProvider> logger
  ) {
    _configuration = configuration.Value;
    _dateTime = dateTime;
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
    _logger = logger;
  }

  /// <inheritdoc />
  public async Task<RefreshTokenSchema> GenerateRefreshTokenAsync(UserId userId, CancellationToken cancellationToken = default) {
    var buffer = new byte[64];
    using var rng = RandomNumberGenerator.Create();
    rng.GetBytes(buffer);

    var refreshToken = Convert.ToBase64String(buffer);
    var expires = _dateTime.UtcNow.AddDays(_configuration.RefreshExpirationInDays);

    try {
      await _unitOfWork.BeginAsync(cancellationToken);
      var user = await _userRepository.FindByIdAsync(userId, cancellationToken);

      if (user is null) {
        throw UserNotFoundException.WithUserId(userId);
      }

      user.WithRefreshToken(refreshToken, expires);
      _userRepository.Update(user);
      await _unitOfWork.CommitAsync(cancellationToken);

      return new RefreshTokenSchema(refreshToken, expires);
    }
    catch (Exception ex) {
      _logger.LogError(ex, "An error occurred while generating the refresh token for user {UserId}", userId);
      await _unitOfWork.RollbackAsync(cancellationToken);
      throw;
    }
  }

  /// <inheritdoc />
  public async Task<bool> ValidateRefreshTokenAsync(string token, CancellationToken cancellationToken = default) {
    var user = await _userRepository.FindByRefreshTokenAsync(token, cancellationToken);

    if (user is null) {
      throw UserNotFoundException.WithRefreshToken(token);
    }

    return user.RefreshToken == token && user.RefreshTokenExpiration > _dateTime.UtcNow;
  }

  /// <inheritdoc />
  public (UserId? userId, string? username) GetUserIdAndUsernameFromAccessToken(string accessToken) {
    var claims = new JwtSecurityTokenHandler()
      .ReadJwtToken(accessToken)
      .Claims
      .ToList();

    var userId = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
    var username = claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName)?.Value;

    if (userId is null ||
        username is null) {
      return (null, null);
    }

    return (UserId.FromString(userId), username);
  }

  /// <inheritdoc />
  public AccessTokenSchema GenerateAccessToken(UserId userId, string username) {
    var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Secret)), SecurityAlgorithms.HmacSha256);
    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub, userId),
      new Claim(JwtRegisteredClaimNames.UniqueName, username),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
    var expires = _dateTime.UtcNow.AddMinutes(_configuration.AccessExpirationInMinutes);
    var securityToken = new JwtSecurityToken(
      _configuration.Issuer,
      _configuration.Audience,
      claims,
      expires: expires,
      signingCredentials: signingCredentials
    );
    var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);

    return new AccessTokenSchema(accessToken, expires);
  }

  /// <inheritdoc />
  public bool ValidateAccessToken(string token)
    => new JwtSecurityTokenHandler().ReadToken(token).ValidTo > _dateTime.UtcNow;

  /// <inheritdoc />
  public async Task RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default) {
    try {
      await _unitOfWork.BeginAsync(cancellationToken);
      var user = await _userRepository.FindByRefreshTokenAsync(refreshToken, cancellationToken);

      if (user is null) {
        throw UserNotFoundException.WithRefreshToken(refreshToken);
      }

      user.WithRefreshToken(null, null);
      _userRepository.Update(user);
      await _unitOfWork.CommitAsync(cancellationToken);
    }
    catch (Exception ex) {
      _logger.LogError(ex, "An error occurred while revoking the refresh token {RefreshToken}", refreshToken);
      await _unitOfWork.RollbackAsync(cancellationToken);
      throw;
    }
  }
}