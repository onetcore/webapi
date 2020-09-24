using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gentings.AspNetCore;
using Gentings.Extensions.Captchas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.WebApis.Security.Login
{
    /// <summary>
    /// 登录。
    /// </summary>
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ICaptchaManager _captchaManager;

        /// <summary>
        /// 初始化类<see cref="LoginController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="roleManager">角色管理接口。</param>
        /// <param name="configuration">配置接口。</param>
        /// <param name="captchaManager">验证码管理接口。</param>
        public LoginController(IUserManager userManager, IRoleManager roleManager, IConfiguration configuration, ICaptchaManager captchaManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
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
                Log(user.Id, "通过账号登录了系统");
            }
            else
            {
                user = await _userManager.FindByPhoneNumberAsync(model.Mobile);
                if (user == null)
                    return BadResult(ErrorCode.InvalidPhoneNumber);
                var captcha = await _captchaManager.GetCaptchaAsync(model.Mobile, "login");
                if (captcha == null)
                    return BadResult(ErrorCode.InvalidCaptcha);
                if (captcha.CaptchaExpiredDate <= DateTimeOffset.Now)
                    return BadResult(ErrorCode.CaptchExpired);
                if (!captcha.Code.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                    return BadResult(ErrorCode.InvalidCaptcha);
                await _userManager.SignInManager.SignInAsync(user, model.AutoLogin);
                Log(user.Id, "通过手机号码登录了系统");
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["Jwt:Expires"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}