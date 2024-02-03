using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Application.Models.Identity;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
    [ApiVersionNeutral]
    [Produces("application/json")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        /// <summary>
        /// User register
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/register
        /// {
        ///     firstName: "Name",
        ///     lastName: "Surname",
        ///     email: "test37@onlinestore.com",
        ///     password: "Password987+",
        ///     passwordConfirmation: "Password987+"
        /// }
        /// </remarks>
        /// <param name="registerRequest">Register request model</param>
        /// <response code="200">Success</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            await _authService.Register(registerRequest);
            return Ok();
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/login
        /// {
        ///     email: "test37@onlinestore.com",
        ///     password: "Password987+",
        /// }
        /// </remarks>
        /// <param name="loginRequest">Login request model</param>
        /// <returns>Returns identity response model</returns>
        /// <response code="200">Success</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResponse>> Login(LoginRequest loginRequest) =>
            Ok(await _authService.Login(loginRequest));

        /// <summary>
        /// Generate new refresh and access tokens for current user
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/refresh
        /// {
        ///     refreshToken: "eyJhbGciOiJIUzI1NiJ9eyJzdWIiOiIxMjM0NTY3ODkwIiwic2VydmVyIjoibmFtZS5jb20iLCJpYXQiOjE2NzAyMTY2NDcsImV4cCI6MTcwMTUyODY0N30q6x6z7e97lR4aZ3413b383116c1497b6f575",
        /// }
        /// </remarks>
        /// <param name="refreshRequest">Refresh request model</param>
        /// <returns>Returns identity response model</returns>
        /// <response code="200">Success</response>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityResponse>> Refresh(RefreshRequest refreshRequest) => 
            Ok(await _authService.Refresh(refreshRequest));

        /// <summary>
        /// User logout
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /auth/logout
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout(UserId);
            return Ok();
        }

        /// <summary>
        /// User email confirmation
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/confirm/email
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     token: "k30354g35gkrgergke34o54355435",
        /// }
        /// </remarks>
        /// <param name="request">Confirm email request model</param>
        /// <response code="200">Success</response>
        [HttpPost("confirm/email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            await _authService.ConfirmEmail(request);
            return Ok();
        }

        /// <summary>
        /// Update user data
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/user/update
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     firstName: "Updated Name",
        ///     lastName: "Updated Surname"
        /// }
        /// </remarks>
        /// <param name="request">Update user request model</param>
        /// <response code="200">Success</response>
        [HttpPost("user/update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            request.UserId = UserId;
            await _authService.UpdateUser(request);
            return Ok();
        }

        /// <summary>
        /// Change user email
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/change/email
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     newEmail: "test37@onlinestore.com"
        /// }
        /// </remarks>
        /// <param name="request">Change email request model</param>
        /// <response code="200">Success</response>
        [HttpPost("change/email")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeEmail(ChangeEmailRequest request)
        {
            request.UserId = UserId;
            await _authService.ChangeEmail(request);
            return Ok();
        }

        /// <summary>
        /// Confirmation of changing user email 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/change/email/confirm
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     token: "k30354g35gkrgergke34o54355435",
        ///     newEmail: "test37@onlinestore.com"
        /// }
        /// </remarks>
        /// <param name="request">Confirm email changing request model</param>
        /// <response code="200">Success</response>
        [HttpPost("change/email/confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmEmailChanging(ConfirmEmailChangingRequest request)
        {
            await _authService.ConfirmEmailChanging(request);
            return Ok();
        }

        /// <summary>
        /// Change old user password to new one
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/change/password
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     oldPassword: "qwerty12345",
        ///     newPassword: "p@ssword999",
        ///     newPasswordConfirmation: "p@ssword999"
        /// }
        /// </remarks>
        /// <param name="request">Change password request model</param>
        /// <response code="200">Success</response>
        [HttpPost("change/password")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            request.UserId = UserId;
            await _authService.ChangePassword(request);
            return Ok();
        }

        /// <summary>
        /// Reset user password request
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/reset/password
        /// </remarks>
        /// <response code="200">Success</response>
        [HttpPost("reset/password/request")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPasswordRequest(string email)
        {
            await _authService.ResetPasswordRequest(email);
            return Ok();
        }

        /// <summary>
        /// Reset user password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /auth/reset/password
        /// {
        ///     userId: "3293EAEE-275B-4331-9A78-30C8816C1BD6",
        ///     token: "k30354g35gkrgergke34o54355435",
        ///     newPassword: "p@ssword999",
        ///     newPasswordConfirmation: "p@ssword999"
        /// }
        /// </remarks>
        /// <param name="request">Reset password request model</param>
        /// <response code="200">Success</response>
        [HttpPost("reset/password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _authService.ResetPassword(request);
            return Ok();
        }
    }
}
