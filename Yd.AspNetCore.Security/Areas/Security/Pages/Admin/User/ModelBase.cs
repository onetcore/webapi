using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.User
{
    /// <summary>
    /// 模型基类。
    /// </summary>
    [Authorize]
    public abstract class ModelBase : AspNetCore.Security.ModelBase
    {

    }
}