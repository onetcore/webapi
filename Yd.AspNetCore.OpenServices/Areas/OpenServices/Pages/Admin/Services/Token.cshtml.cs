using System;
using Gentings.Extensions.OpenServices.ApiDocuments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.OpenServices.Areas.OpenServices.Pages.Admin.Services
{
    /// <summary>
    /// 设置Token。
    /// </summary>
    public class TokenModel : AdminModelBase
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Token标识。
            /// </summary>
            public string Token { get; set; }
        }

        /// <summary>
        /// 模型实例。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// 获取Token页面。
        /// </summary>
        public void OnGet()
        {
            if (HttpContext.Request.Cookies.TryGetValue(ApiDescriptor.JwtToken, out var token))
                Input = new InputModel { Token = token };
        }

        /// <summary>
        /// 设置Token实例。
        /// </summary>
        /// <returns>返回设置结果。</returns>
        public IActionResult OnPost()
        {
            HttpContext.Response.Cookies.Delete(ApiDescriptor.JwtToken);
            HttpContext.Response.Cookies.Append(ApiDescriptor.JwtToken, Input.Token, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1)
            });
            return Success("你已经成功设置了Token！");
        }
    }
}
