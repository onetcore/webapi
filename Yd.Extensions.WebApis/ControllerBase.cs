using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.WebApis
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBase : Extensions.ControllerBase
    {
    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    [ApiController]
    [Area("account")]
    [Route("api/[account]/[controller]")]
    public abstract class AccountControllerBase : Extensions.ControllerBase
    {
    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    [ApiController]
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    public abstract class AdminControllerBase : Extensions.ControllerBase
    {
    }
}