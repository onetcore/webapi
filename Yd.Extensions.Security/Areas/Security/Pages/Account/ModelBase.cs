using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.Security.Areas.Security.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : Security.ModelBase
    {

    }
}