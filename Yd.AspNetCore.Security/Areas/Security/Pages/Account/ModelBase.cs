using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Account
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : AspNetCore.Security.ModelBase
    {

    }
}