using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.Security.Areas.Security.Pages
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
