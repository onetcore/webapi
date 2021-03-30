﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gentings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Account
{
    public class DownloadPersonalDataModel : ModelBase
    {
        private readonly IUserManager _userManager;
        public DownloadPersonalDataModel(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync();
            if (user == null)
            {
                return NotFound("用户不存在！");
            }

            Log("获取{0}的私有数据！", user.UserName);

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(User).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var login in logins)
            {
                personalData.Add($"{login.LoginProvider} 登录密钥", login.ProviderKey);
            }

            personalData.Add("验证密钥", await _userManager.GetAuthenticatorKeyAsync(user));

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(Encoding.UTF8.GetBytes(personalData.ToJsonString()), "text/json");
        }
    }
}
