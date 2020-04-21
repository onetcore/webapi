using Gentings.Identity.Permissions;
using Yd.Extensions;

namespace Yd.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(Permissions.Administrator)]
    public abstract class ModelBase : Yd.Extensions.RazorPages.ModelBase
    {
    }
}