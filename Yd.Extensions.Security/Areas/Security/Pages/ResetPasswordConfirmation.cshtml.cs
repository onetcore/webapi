using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.Security.Areas.Security.Pages
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
