namespace Yd.Extensions.Security
{
    /// <summary>
    /// 默认角色。
    /// </summary>
    public enum DefaultRoles
    {
        /// <summary>
        /// 普通会员。
        /// </summary>
        Members = 0,
        /// <summary>
        /// 管理员。
        /// </summary>
        Moderators = 1,
        /// <summary>
        /// 系统管理员。
        /// </summary>
        Administrators = 1000000,
        /// <summary>
        /// 开发者。
        /// </summary>
        Developers = int.MaxValue,
    }
}