using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.RazorPages.Areas.OpenServices.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : OpenServices.ModelBase
    {

    }
}