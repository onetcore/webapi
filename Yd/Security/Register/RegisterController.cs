using System;
using System.Threading.Tasks;
using Gentings.Identity;
using Gentings.Identity.Captchas;
using Gentings.Identity.Events;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Properties;
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
        private readonly IEventLogger _logger;

        /// <summary>
        /// 初始化类<see cref="RegisterController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="captchaManager">短信验证码管理接口。</param>
        /// <param name="logger">用户日志。</param>
        public RegisterController(IUserManager userManager, ICaptchaManager captchaManager, IEventLogger logger)
        {
            _userManager = userManager;
            _captchaManager = captchaManager;
            _logger = logger;
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
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var captcha = await _captchaManager.GetCaptchaAsync(model.Mobile, "login");
            if (captcha == null)
                return BadResult(ErrorCode.InvalidCaptcha);
            if (captcha.CaptchaExpiredDate <= DateTimeOffset.Now)
                return BadResult(ErrorCode.CaptchExpired);
            if (!captcha.Code.Equals(model.Captcha, StringComparison.OrdinalIgnoreCase))
                return BadResult(ErrorCode.InvalidCaptcha);
            var user = new User();
            user.UserName = model.UserName;
            user.Email = model.Mail;
            user.PhoneNumber = model.Mobile;
            if (model.Prefix != "86")
                user.PhoneNumber = model.Prefix + " " + user.PhoneNumber;
            user.PhoneNumberConfirmed = true;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                _logger.LogUser(user.Id, Resources.Register_Success);
                return OkResult();
            }
            return BadResult(ErrorCode.RegisterFailured,result.ToErrorString());
        }
    }
}