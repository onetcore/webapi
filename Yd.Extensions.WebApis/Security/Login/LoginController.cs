using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Gentings.AspNetCore;
using Gentings.Extensions.SMS.Captchas;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;
using Yd.Extensions.WebApis.Properties;

namespace Yd.Extensions.WebApis.Security.Login
{
    /// <summary>
    /// 登录。
    /// </summary>
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly ICaptchaManager _captchaManager;

        /// <summary>
        /// 初始化类<see cref="LoginController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="roleManager">角色管理接口。</param>
        /// <param name="captchaManager">验证码管理接口。</param>
        public LoginController(IUserManager userManager, IRoleManager roleManager, ICaptchaManager captchaManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _captchaManager = captchaManager;
        }

        /// <summary>
        /// 发送登录API。
        /// </summary>
        /// <param name="model">登录模型。</param>
        /// <returns>返回登录结果。</returns>
        [HttpPost]
        [ApiResult]
        [ApiResult(typeof(LoginResult))]
        public async Task<IActionResult> Post([FromBody] LoginModel model)
        {
            User user;
            if (model.Type.ToLower().Trim() == "account")
            {
                user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return BadResult(ErrorCode.InvalidUserNameOrPassword);
                var result = await _userManager.PasswordSignInAsync(user, model.Password, model.AutoLogin);
                if (!result.Succeeded)
                    return BadResult(ErrorCode.InvalidUserNameOrPassword);
                await Events.LogAsync(@event =>
                {
                    @event.UserId = UserId;
                    @event.Message = Resources.Login_Account_Success;
                }, EventType);
            }
            else
            {
                user = await _userManager.FindByPhoneNumberAsync(model.Mobile);
                if (user == null)
                    return BadResult(ErrorCode.InvalidPhoneNumber);
                var captcha = await _captchaManager.GetCaptchaAsync(model.Mobile, "login");
                if (captcha == null)
                    return BadResult(ErrorCode.InvalidCaptcha);
                if (captcha.ExpiredDate <= DateTimeOffset.Now)
                    return BadResult(ErrorCode.CaptchExpired);
                if (!captcha.Code.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                    return BadResult(ErrorCode.InvalidCaptcha);
                await _userManager.SignInManager.SignInAsync(user, model.AutoLogin);
                await Events.LogAsync(@event =>
                {
                    @event.UserId = UserId;
                    @event.Message = Resources.Login_Mobile_Success;
                }, EventType);
            }

            await _userManager.SetLoginStatusAsync(user);
            return OkResult(new LoginResult
            {
                Type = model.Type,
                Token = GetToken(user),
                Authority = await _roleManager.GetAuthorityAsync(user.RoleId)
            });
        }

        private string GetToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };
            return CreateJwtSecurityToken(claims);
        }
    }
}