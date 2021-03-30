using Gentings.Security.Permissions;

namespace Yd.AspNetCore.OpenServices
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(OpenServicePermissions.View)]
    public abstract class AccountModelBase : ModelBase
    {

    }
}