using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gentings.Identity.Captchas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Yd.Extensions.Security;
using ControllerBase = Gentings.AspNetCore.ControllerBase;

namespace Yd.Security.Login
{
    /// <summary>
    /// 登录。
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICaptchaManager _captchaManager;

        /// <summary>
        /// 初始化类<see cref="LoginController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="configuration">配置接口。</param>
        /// <param name="captchaManager">验证码管理接口。</param>
        public LoginController(IUserManager userManager, IConfiguration configuration, ICaptchaManager captchaManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _captchaManager = captchaManager;
        }

        /// <summary>
        /// 获取手机验证码。
        /// </summary>
        /// <param name="mobile">电话号码。</param>
        /// <returns>返回是否成功获取验证码。</returns>
        [HttpGet("captcha")]
        public async Task<IActionResult> GetCaptcha(string mobile)
        {
            var user = await _userManager.FindByPhoneNumberAsync(mobile);
            if (user == null)
                return BadResult(ErrorCode.InvalidPhoneNumber);
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();
            var success = await _captchaManager.SaveCaptchAsync(mobile, "login", code, 3);
            return Ok(success);
        }

        /// <summary>
        /// 发送登录API。
        /// </summary>
        /// <param name="model">登录模型。</param>
        /// <returns>返回登录结果。</returns>
        [HttpPost]
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
            }

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

            return OkResult(new LoginResult { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}