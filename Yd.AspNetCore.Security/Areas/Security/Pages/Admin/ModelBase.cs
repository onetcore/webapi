using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize]
    public abstract class ModelBase : AspNetCore.Security.ModelBase
    {

    }
}