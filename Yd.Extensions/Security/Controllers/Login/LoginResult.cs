using Gentings.AspNetCore;

namespace Yd.Extensions.Security.Controllers.Login
{
    /// <summary>
    /// 登录结果。
    /// </summary>
    public class LoginResult : ApiResult
    {
        /// <summary>
        /// 标签。
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户验证。
        /// </summary>
        public string[] Authority { get; set; }

        /// <summary>
        /// 登录类型。
        /// </summary>
        public string Type { get; set; }
    }
}