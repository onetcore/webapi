using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Storages
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(StoragePermissions.View)]
    public abstract class AdminModelBase : ModelBase
    {
    }
}