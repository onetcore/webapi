using Microsoft.AspNetCore.Authorization;

namespace Yd.Security.Account
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [Authorize]
    public abstract class ControllerBase : Security.ControllerBase
    {

    }
}