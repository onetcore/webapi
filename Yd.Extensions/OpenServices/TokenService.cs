using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Gentings.AspNetCore;
using Gentings.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Yd.Extensions.OpenServices
{
    /// <summary>
    /// Token服务。
    /// </summary>
    public class TokenService : ServiceBase
    {
        private readonly IApplicationManager _applicationManager;
        /// <summary>
        /// 初始化类<see cref="TokenService"/>。
        /// </summary>
        /// <param name="applicationManager">应用管理实例。</param>
        public TokenService(IApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        /// <summary>
        /// 验证模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// AppId。
            /// </summary>
            [Required(ErrorMessage = "AppId不能为空！")]
            public Guid AppId { get; set; }

            /// <summary>
            /// 密钥。
            /// </summary>
            [Required(ErrorMessage = "密钥不能为空！")]
            public string AppSecret { get; set; }
        }

        /// <summary>
        /// 获取验证Token。
        /// </summary>
        /// <param name="input">验证模型实例。</param>
        /// <returns>返回Token实例。</returns>
        [HttpPost]
        [AllowAnonymous]
        [ApiDataResult(typeof(TokenResult))]
        public async Task<IActionResult> Index([FromBody] InputModel input)
        {
            if (!ModelState.IsValid)
                return BadResult();

            var application = await _applicationManager.FindUserApplicationAsync(input.AppId);
            if (application == null)
                return BadResult(ErrorCode.ApplicationNotFound);

            if (!application.AppSecret.Equals(input.AppSecret, StringComparison.OrdinalIgnoreCase))
                return BadResult(ErrorCode.AppSecretInvalid);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, application.UserId.ToString()),
                new Claim(ClaimTypes.Name, application["UserName"]),
                new Claim(ClaimTypes.Sid, application.Id.ToString("N"))
            };
            var result = GetRequiredService<IConfiguration>().CreateJwtSecurityToken(claims);
            return OkResult(new TokenResult(result));
        }
    }
}