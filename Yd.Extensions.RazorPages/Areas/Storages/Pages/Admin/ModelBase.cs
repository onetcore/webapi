﻿using Gentings.Identity.Permissions;
using Yd.Extensions.RazorPages.Properties;

namespace Yd.Extensions.RazorPages.Areas.Storages.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(Permissions.Administrator)]
    public abstract class ModelBase : Security.ModelBase
    {
        /// <summary>
        /// 事件类型。
        /// </summary>
        protected override string EventType => Resources.EventType_Storages;
    }
}