using Microsoft.AspNetCore.Mvc;
using ControllerBase = Gentings.AspNetCore.ControllerBase;

namespace Yd.Security.Register
{
    /// <summary>
    /// 注册。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        /// <summary>
        /// 发送注册API。
        /// </summary>
        /// <param name="model">注册模型。</param>
        /// <returns>返回注册结果。</returns>
        [HttpPost]
        public IActionResult Post([FromBody] RegisterModel model)
        {
            return Ok(new RegisterResult());
        }
    }
}