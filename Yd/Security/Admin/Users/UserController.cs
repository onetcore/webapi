using System.Threading.Tasks;
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
        /// <summary>
        /// 初始化类<see cref="UserController"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
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
            if (model.Password != model.Confirm)
                return BadResult("密码和确认密码不匹配！");
            var user = new User();
            user.UserName = model.UserName;
            user.RealName = model.RealName ?? model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Summary = model.Summary;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }

        /// <summary>
        /// 更新用户。
        /// </summary>
        /// <param name="model">用户模型。</param>
        /// <returns>返回更新结果。</returns>
        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateUserModel model)
        {
            if (model.Password != null && model.Password != model.Confirm)
                return BadResult("密码和确认密码不匹配！");
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null || user.UserName != model.UserName)
                return BadResult(ErrorCode.UserNotFound);
            if (model.Password != null)
            {
                var resetResult = await _userManager.ResetPasswordAsync(user, model.Password);
                if (!resetResult.Succeeded)
                    return BadResult(resetResult.ToErrorString());
            }

            user.RealName = model.RealName ?? user.RealName ?? user.UserName;
            user.Summary = model.Summary;
            user.Email = model.Email;
            user.NormalizedEmail = null;
            user.PhoneNumber = model.PhoneNumber;
            user.PhoneNumberConfirmed = user.PhoneNumber != null;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return OkResult();
            return BadResult(result.ToErrorString());
        }
    }
}