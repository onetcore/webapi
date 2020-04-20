using Gentings.Identity.Permissions;
using Yd.AspNetCore.RazorPages.Properties;
using Yd.Extensions;

namespace Yd.AspNetCore.RazorPages.Areas.Storages.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(Permissions.Administrator)]
    public abstract class ModelBase : RazorPages.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Storages;
    }
}