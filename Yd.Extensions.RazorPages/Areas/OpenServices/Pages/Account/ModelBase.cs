using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(OpenServicePermissions.View)]
    public abstract class ModelBase : OpenServices.ModelBase
    {

    }
}