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
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    public abstract class AdminControllerBase : ControllerBase
    {

    }

    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    [Area("account")]
    [Route("api/[area]/[controller]")]
    public abstract class AccountControllerBase : ControllerBase
    {

    }

}