using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.WebApis.Security.Forget
{
    /// <summary>
    /// 忘记密码。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ForgetController : ControllerBase
    {
        /// <summary>
        /// 发送忘记密码API。
        /// </summary>
        /// <param name="model">忘记密码模型。</param>
        /// <returns>返回忘记密码结果。</returns>
        [HttpPost]
        public IActionResult Post([FromBody] ForgetModel model)
        {
            return Ok(new ForgetResult());
        }
    }
}