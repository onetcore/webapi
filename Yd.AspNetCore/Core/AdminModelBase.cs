using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Core
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize]
    public abstract class AdminModelBase : ModelBase
    {
    }
}