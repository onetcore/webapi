using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin.User
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : RazorPages.ModelBase
    {

    }
}