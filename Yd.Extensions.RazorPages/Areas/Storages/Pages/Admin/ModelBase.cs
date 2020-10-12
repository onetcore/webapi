using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.Storages.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(StoragePermissions.View)]
    public abstract class ModelBase : RazorPages.ModelBase
    {
    }
}