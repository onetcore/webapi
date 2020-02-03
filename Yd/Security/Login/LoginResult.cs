using Gentings.AspNetCore;

namespace Yd.Security.Login
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
        /// 登录类型。
        /// </summary>
        public string Type { get; set; }
    }
}