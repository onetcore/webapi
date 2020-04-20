using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
