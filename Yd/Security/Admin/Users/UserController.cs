using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Gentings.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Security.Admin.Users
{
    /// <summary>
    /// 用户控制器。
    /// </summary>
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ISettingsManager _settingsManager;

        /// <summary>
        /// 初始化类<see cref="UserController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="settingsManager">设置管理接口。</param>
        public UserController(IUserManager userManager, ISettingsManager settingsManager)
        {
            _userManager = userManager;
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// 获取更新用户所用到的实例。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回用户实例。</returns>
        [HttpGet("get-update")]
        public async Task<IActionResult> GetUpdate(int id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadResult(ErrorCode.UserNotFound);
            return OkResult(new UpdateUserModel
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RealName = user.RealName,
                UserName = user.UserName,
                Summary = user.Summary,
                LockoutEnabled = user.LockoutEnabled,
            });
        }

        /// <summary>
        /// 添加用户。
        /// </summary>
        /// <param name="model">用户模型。</param>
        /// <returns>返回添加结果。</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var settings = await _settingsManager.GetSettingsAsync<SecuritySettings>();
                var user = new User();
                user.UserName = model.UserName;
                user.RealName = model.RealName ?? model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Summary = model.Summary;
                user.LockoutEnabled = true;
                user.EmailConfirmed = !settings.RequiredEmailConfirmed || !string.IsNullOrEmpty(model.Email);
                user.PhoneNumberConfirmed = !settings.RequiredPhoneNumberConfirmed || !string.IsNullOrEmpty(model.PhoneNumber);
                user.TwoFactorEnabled = settings.RequiredTwoFactorEnabled;
                user.Type = UserType.Normal;
                user.Level = User.Level + 1;
                user.ParentId = UserId;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    Log("添加用户：{0}({1})", user.RealName, user.UserName);
                    return OkResult();
                }
                return BadResult(result.ToErrorString());
            }

            return BadResult();
        }

        /// <summary>
        /// 更新用户。
        /// </summary>
        /// <param name="model">用户模型。</param>
        /// <returns>返回更新结果。</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                    return BadResult(ErrorCode.UserNotFound);
                if (model.Password == null && user.UserName != model.UserName)
                    return BadResult("用户名称改变同时需要修改密码！");

                if (user.UserName != model.UserName)
                {
                    user.UserName = model.UserName;
                    user.NormalizedUserName = null;
                }
                if (model.Password != null)
                {
                    user.PasswordHash = model.Password;
                    user.PasswordHash = _userManager.HashPassword(user);
                }
                user.RealName = model.RealName ?? user.RealName ?? user.UserName;
                user.Summary = model.Summary;
                if (user.Email != model.Email)
                {
                    user.Email = model.Email;
                    user.NormalizedEmail = null;
                }

                var settings = await _settingsManager.GetSettingsAsync<SecuritySettings>();
                user.LockoutEnabled = model.LockoutEnabled;
                user.PhoneNumber = model.PhoneNumber;
                user.EmailConfirmed = !settings.RequiredEmailConfirmed || !string.IsNullOrEmpty(model.Email);
                user.PhoneNumberConfirmed = !settings.RequiredPhoneNumberConfirmed || !string.IsNullOrEmpty(model.PhoneNumber);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    Log("更新用户：{0}({1})", user.RealName, user.UserName);
                    return OkResult();
                }
                return BadResult(result.ToErrorString());
            }

            return BadResult();
        }
    }
}