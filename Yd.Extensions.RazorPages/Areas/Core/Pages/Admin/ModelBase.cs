﻿using Gentings.Identity.Permissions;
using Yd.Extensions;
using Yd.Extensions.Security;

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