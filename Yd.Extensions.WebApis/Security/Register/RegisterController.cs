﻿using System;
using System.Threading.Tasks;
using Gentings.Extensions.SMS.Captchas;
using Gentings.Security;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.WebApis.Properties;

namespace Yd.Extensions.WebApis.Security.Register
{
    /// <summary>
    /// 注册。
    /// </summary>
    public class RegisterController : Extensions.Security.ControllerBase
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
        /// 发送注册API。
        /// </summary>
        /// <param name="model">注册模型。</param>
        /// <returns>返回注册结果。</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var captcha = await _captchaManager.GetCaptchaAsync(model.PhoneNumber, "register");
            if (captcha == null)
                return BadResult(ErrorCode.InvalidCaptcha);
            if (captcha.ExpiredDate <= DateTimeOffset.Now)
                return BadResult(ErrorCode.CaptchExpired);
            if (!captcha.Code.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                return BadResult(ErrorCode.InvalidCaptcha);

            var user = new User();
            user.UserName = model.UserName;
            user.NickName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = true;
            //邀请码
            if (!string.IsNullOrEmpty(model.InviteKey))
            {

            }

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await Events.LogAsync(@event =>
                {
                    @event.UserId = UserId;
                    @event.Message = Resources.Register_Success;
                }, EventType);
                return OkResult();
            }
            return BadResult(ErrorCode.RegisterFailured, result.ToErrorString());
        }
    }
}