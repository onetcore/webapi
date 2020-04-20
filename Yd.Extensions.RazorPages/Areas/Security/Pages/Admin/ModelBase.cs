using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(Extensions.Permissions.Administrator)]
    public abstract class ModelBase : Security.ModelBase
    {

    }
}