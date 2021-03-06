﻿using Gentings.AspNetCore;

namespace Yd.Extensions.WebApis.Security.Forget
{
    /// <summary>
    /// 登录结果。
    /// </summary>
    public class ForgetResult : ApiResult
    {
        /// <summary>
        /// 标签。
        /// </summary>
        public string Token { get; set; }
    }
}