﻿using System;
using System.Threading.Tasks;
using Gentings.Identity.Captchas;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
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
        private readonly IUserManager _userManager;
        private readonly ICaptchaManager _captchaManager;
        /// <summary>
        /// 初始化类<see cref="RegisterController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="captchaManager">短信验证码管理接口。</param>
        public RegisterController(IUserManager userManager, ICaptchaManager captchaManager)
        {
            _userManager = userManager;
            _captchaManager = captchaManager;
        }

        /// <summary>
        /// 获取手机验证码。
        /// </summary>
        /// <param name="mobile">电话号码。</param>
        /// <param name="prefix">电话号码区码，如+86。</param>
        /// <returns>返回是否成功获取验证码。</returns>
        [HttpGet("captcha")]
        public async Task<IActionResult> GetCaptcha(string mobile, string prefix)
        {
            var user = await _userManager.FindByPhoneNumberAsync(mobile);
            if (user != null)
                return BadResult(ErrorCode.PhoneNumberAlreadyExisted);
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();
            var success = await _captchaManager.SaveCaptchAsync(mobile, "register", code, 3);
            if (success)
                return OkResult();
            return BadResult(ErrorCode.GetCaptchaFailured);
        }

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