using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Application.Interfaces.Identity;
using OnlineStore.Application.Models.Identity;
using OnlineStore.WebAPI.Controllers.Base;

namespace OnlineStore.WebAPI.Controllers
{
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
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost("refresh")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IdentityResponse>> Refresh(RefreshRequest refreshRequest) => 
            Ok(await _authService.Refresh(refreshRequest, UserId));

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
    }
}
