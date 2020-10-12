using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.Emails.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(EmailPermissions.Email)]
    public abstract class ModelBase : Emails.ModelBase
    {

    }
}