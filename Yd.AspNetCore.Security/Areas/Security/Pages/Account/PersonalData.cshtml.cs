﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Account
{
    public class PersonalDataModel : ModelBase
    {
        private readonly IUserManager _userManager;

        public PersonalDataModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync();
            if (user == null)
            {
                return NotFound("用户不存在！");
            }

            return Page();
        }
    }
}
