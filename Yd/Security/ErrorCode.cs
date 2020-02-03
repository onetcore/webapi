namespace Yd.Security
{
    /// <summary>
    /// 错误编码。
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 用户名或者密码错误！
        /// </summary>
        InvalidUserNameOrPassword = 10000,
        /// <summary>
        /// 电话号码不存在！
        /// </summary>
        InvalidPhoneNumber = 10001,
        /// <summary>
        /// 验证码错误！
        /// </summary>
        InvalidCaptcha = 10002,
        /// <summary>
        /// 验证码过期。
        /// </summary>
        CaptchExpired = 10003,
    }
}