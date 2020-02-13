namespace Yd.Extensions.Security.Controllers
{
    /// <summary>
    /// 错误编码。
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 用户名或者密码错误。
        /// </summary>
        InvalidUserNameOrPassword = 10000,
        /// <summary>
        /// 电话号码不存在。
        /// </summary>
        InvalidPhoneNumber,
        /// <summary>
        /// 验证码错误。
        /// </summary>
        InvalidCaptcha,
        /// <summary>
        /// 短信验证码发送错误。
        /// </summary>
        GetCaptchaFailured,
        /// <summary>
        /// 验证码过期。
        /// </summary>
        CaptchExpired,
        /// <summary>
        /// 电话号码已经存在。
        /// </summary>
        PhoneNumberAlreadyExisted,
        /// <summary>
        /// 注册失败。
        /// </summary>
        RegisterFailured,
        /// <summary>
        /// 角色未找到。
        /// </summary>
        RoleNotFound,
        /// <summary>
        /// 用户未找到。
        /// </summary>
        UserNotFound,
    }
}