using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace OnlineStore.WebAPI.Controllers.Base
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {
        internal Guid UserId => (User.Identity?.IsAuthenticated ?? false)
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty)
            : Guid.Empty;
    }
}
