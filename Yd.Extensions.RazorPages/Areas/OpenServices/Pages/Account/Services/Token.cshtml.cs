using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.ApiOpenServices;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account.Services
{
    /// <summary>
    /// ����Token��
    /// </summary>
    public class TokenModel : ModelBase
    {
        /// <summary>
        /// ����ģ�͡�
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Token��ʶ��
            /// </summary>
            public string Token { get; set; }
        }

        /// <summary>
        /// ģ��ʵ����
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// ��ȡTokenҳ�档
        /// </summary>
        public void OnGet()
        {
            if (HttpContext.Request.Cookies.TryGetValue(ApiDescriptor.JwtToken, out var token))
                Input = new InputModel { Token = token };
        }

        /// <summary>
        /// ����Tokenʵ����
        /// </summary>
        /// <returns>�������ý����</returns>
        public IActionResult OnPost()
        {
            HttpContext.Response.Cookies.Delete(ApiDescriptor.JwtToken);
            HttpContext.Response.Cookies.Append(ApiDescriptor.JwtToken, Input.Token, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1)
            });
            return Success("���Ѿ��ɹ�������Token��");
        }
    }
}