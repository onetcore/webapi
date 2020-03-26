using Microsoft.AspNetCore.Authorization;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages
{
    [AllowAnonymous]
    public class LockoutModel : ModelBase
    {
        public void OnGet()
        {
        }
    }
}
