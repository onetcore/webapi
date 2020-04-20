using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : RazorPages.ModelBase
    {

    }
}