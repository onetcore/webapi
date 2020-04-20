using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
