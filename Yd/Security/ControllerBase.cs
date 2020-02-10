using Microsoft.AspNetCore.Mvc;

namespace Yd.Security
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ControllerBase : Extensions.ControllerBase
    {

    }
}