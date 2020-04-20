using Microsoft.AspNetCore.Authorization;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages
{
    [AllowAnonymous]
    public class LockoutModel : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
