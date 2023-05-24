// Copyright (c) Bruno Sales <me@baliestri.dev>. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

using Calliope.Application.Schemas.Requests.Authentication;
using Calliope.Application.Schemas.Responses.Authentication;
using Calliope.Application.Schemas.Responses.Generic;
using Calliope.Application.UseCases.Authentication.Register;
using Calliope.Application.UseCases.Authentication.RenewToken;
using Calliope.Application.UseCases.Authentication.SignIn;
using Calliope.Application.UseCases.Authentication.SignOut;
using Calliope.WebAPI.Abstractions.Controller;
using Calliope.WebAPI.Attributes;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Calliope.WebAPI.Controllers;

/// <summary>
///   Represents an authentication controller.
/// </summary>
public sealed class AuthController : BaseController {
  private readonly ILogger<AuthController> _logger;
  private readonly IMapper _mapper;
  private readonly IMediator _mediator;

  /// <summary>
  ///   Initializes a new instance of the <see cref="AuthController" /> class.
  /// </summary>
  /// <param name="logger">The injected <see cref="ILogger{TCategoryName}" />.</param>
  /// <param name="mapper">The injected <see cref="IMapper" />.</param>
  /// <param name="mediator">The injected <see cref="IMediator" />.</param>
  public AuthController(ILogger<AuthController> logger, IMapper mapper, IMediator mediator) {
    _logger = logger;
    _mapper = mapper;
    _mediator = mediator;
  }

  /// <summary>
  ///   Register a new user.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The register response.</returns>
  /// <response code="201">The user was created.</response>
  /// <response code="409">The user already exists.</response>
  /// <response code="422">The request is invalid.</response>
  /// <response code="500">An error occurred while processing the request.</response>
  [HttpPost("register")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(RegisterResponseSchema), 201)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 409)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 422)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 500)]
  public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestSchema request, CancellationToken cancellationToken = default) {
    var useCase = _mapper.Map<RegisterUseCase>(request);
    var errorOr = await _mediator.Send(useCase, cancellationToken);

    return errorOr.Match(response => CreatedAtAction("Register", response), HandleProblem);
  }

  /// <summary>
  ///   Sign in a user.
  /// </summary>
  /// <param name="request">The request.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The sign in response.</returns>
  /// <response code="200">The user was authenticated.</response>
  /// <response code="401">The user was not authenticated.</response>
  /// <response code="422">The request is invalid.</response>
  /// <response code="500">An error occurred while processing the request.</response>
  [HttpPost("sign-in")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(SignInResponseSchema), 200)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 401)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 422)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 500)]
  public async Task<IActionResult> SignInAsync([FromBody] SignInRequestSchema request, CancellationToken cancellationToken = default) {
    var useCase = _mapper.Map<SignInUseCase>(request);
    var errorOr = await _mediator.Send(useCase, cancellationToken);

    return errorOr.Match(response => Ok(response), HandleProblem);
  }

  /// <summary>
  ///   Renew access and refresh tokens.
  /// </summary>
  /// <param name="authorization">The authorization header.</param>
  /// <param name="refreshToken">The refresh token.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The renew token response.</returns>
  /// <response code="201">The tokens were renewed.</response>
  /// <response code="401">The user was not authenticated.</response>
  /// <response code="422">The request is invalid.</response>
  /// <response code="500">An error occurred while processing the request.</response>
  [HttpGet("renew")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(SignInResponseSchema), 201)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 422)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 500)]
  public async Task<IActionResult> RenewTokenAsync(
    [ParameterIgnore] [FromHeader(Name = "Authorization")]
    string authorization,
    [FromHeader(Name = "RefreshToken")] string refreshToken,
    CancellationToken cancellationToken = default
  ) {
    var accessToken = authorization.Replace("Bearer ", string.Empty);
    var request = new RenewTokenRequestSchema(accessToken, refreshToken);
    var useCase = _mapper.Map<RenewTokenUseCase>(request);
    var errorOr = await _mediator.Send(useCase, cancellationToken);

    return errorOr.Match(response => Ok(response), HandleProblem);
  }

  /// <summary>
  ///   Sign out a user.
  /// </summary>
  /// <param name="refreshToken">The refresh token.</param>
  /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
  /// <returns>The sign out response.</returns>
  /// <response code="200">The user was signed out.</response>
  /// <response code="401">The user was not authenticated.</response>
  /// <response code="422">The request is invalid.</response>
  /// <response code="500">An error occurred while processing the request.</response>
  [Authorize]
  [HttpGet("sign-out")]
  [MapToApiVersion("1.0")]
  [Produces("application/json")]
  [ProducesResponseType(typeof(ProblemResponseSchema), 422)]
  [ProducesResponseType(typeof(ProblemResponseSchema), 500)]
  public async Task<IActionResult> SignOutAsync([FromHeader(Name = "RefreshToken")] string refreshToken, CancellationToken cancellationToken = default) {
    var request = new SignOutRequestSchema(refreshToken);
    var useCase = _mapper.Map<SignOutUseCase>(request);
    var errorOr = await _mediator.Send(useCase, cancellationToken);

    return errorOr.Match(response => Ok(response), HandleProblem);
  }
}