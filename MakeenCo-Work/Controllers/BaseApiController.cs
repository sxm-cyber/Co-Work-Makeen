using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace MakeenCo_Work.Controllers;

[ApiController , Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        var idValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(idValue))
            throw new UnauthorizedAccessException("User Is not Authenticated");

        return Guid.Parse(idValue);
    }

    protected string? GetCurrentUserPhoneNumber()
        => User.FindFirstValue(ClaimTypes.MobilePhone);

    protected bool IsAuthenticated()
        => User?.Identity?.IsAuthenticated == true;

}     