﻿using Gentings.Identity.Permissions;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [PermissionAuthorize(Permissions.Administrator)]
    public abstract class ModelBase : RazorPages.ModelBase
    {
    }
}