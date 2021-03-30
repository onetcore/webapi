using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Emails
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(EmailPermissions.Email)]
    public abstract class AdminModelBase : ModelBase
    {

    }
}