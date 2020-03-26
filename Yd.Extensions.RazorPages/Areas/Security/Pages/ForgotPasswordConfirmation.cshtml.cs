using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
