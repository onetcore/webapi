using System;
using System.Threading.Tasks;
using Gentings.AspNetCore;
using Gentings.Extensions.SMS.Captchas;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.Controllers
{
    /// <summary>
    /// 短信验证码。
    /// </summary>
    public class CaptchaController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ICaptchaManager _captchaManager;

        /// <summary>
        /// 初始化类<see cref="CaptchaController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="captchaManager">验证码管理接口。</param>
        public CaptchaController(IUserManager userManager, ICaptchaManager captchaManager)
        {
            _userManager = userManager;
            _captchaManager = captchaManager;
        }

        /// <summary>
        /// 获取手机验证码。
        /// </summary>
        /// <param name="mobile">电话号码。</param>
        /// <param name="type">类型。</param>
        /// <returns>返回是否成功获取验证码。</returns>
        [ApiResult]
        [HttpGet("{type:alpha?}")]
        public async Task<IActionResult> GetCaptcha(string mobile, string type = null)
        {
            if(type?.ToLower() != "register")
            {
                var user = await _userManager.FindByPhoneNumberAsync(mobile);
                if (user == null)
                    return BadResult(ErrorCode.InvalidPhoneNumber);
            }
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();
            var success = await _captchaManager.SaveCaptchAsync(mobile, type ?? "login", code, 3);
            if (success)
                return OkResult();
            return BadResult(ErrorCode.GetCaptchaFailured);
        }
    }
}