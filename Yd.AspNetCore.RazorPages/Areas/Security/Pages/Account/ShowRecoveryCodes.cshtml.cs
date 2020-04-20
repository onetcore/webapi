using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.RazorPages.Areas.Security.Pages.Account
{
    public class ShowRecoveryCodesModel : ModelBase
    {
        [TempData]
        public string[] RecoveryCodes { get; set; }
        
        public IActionResult OnGet()
        {
            if (RecoveryCodes == null || RecoveryCodes.Length == 0)
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }

            return Page();
        }
    }
}
