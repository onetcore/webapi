using Gentings.Identity.Permissions;

namespace Yd.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize]
    public abstract class ModelBase : Extensions.RazorPages.ModelBase
    {
    }
}