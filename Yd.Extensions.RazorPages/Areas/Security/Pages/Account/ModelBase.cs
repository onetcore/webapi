using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : Security.ModelBase
    {

    }
}