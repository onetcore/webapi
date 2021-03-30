using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Gentings.Security.Permissions;
using Gentings.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yd.AspNetCore.Core;
using Yd.Extensions;

namespace Yd.AspNetCore.Areas.Core.Pages.Admin
{
    /// <summary>
    /// 网站配置。
    /// </summary>
    [PermissionAuthorize(CorePermissions.SiteSettings)]
    public class SettingsModel : AdminModelBase
    {
        private readonly ISettingsManager _settingsManager;
        /// <summary>
        /// 初始化类<see cref="SettingsModel"/>。
        /// </summary>
        /// <param name="settingsManager">配置管理接口。</param>
        public SettingsModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        /// <summary>
        /// 配置输入模型。
        /// </summary>
        [BindProperty]
        public SiteSettings Input { get; set; }

        /// <summary>
        /// 获取当前网站配置。
        /// </summary>
        public void OnGet()
        {
            Input = SiteSettings;
        }

        /// <summary>
        /// 保存配置实例。
        /// </summary>
        /// <returns>返回保存结果。</returns>
        public IActionResult OnPost()
        {
            var valid = true;
            if (string.IsNullOrEmpty(Input.SiteName))
            {
                valid = false;
                ModelState.AddModelError("Input.SiteName", "网站名称不能为空！");
            }

            if (valid)
            {//需要把可修改的属性全服附加到对象上再更新
                var settings = SiteSettings;
                settings.Copyright = Input.Copyright;
                settings.IsTopMenu = Input.IsTopMenu;
                settings.SiteName = Input.SiteName;
                settings.ShortName = Input.ShortName;
                settings.LogoUrl = Input.LogoUrl;
                settings.Description = Input.Description;
                if (_settingsManager.SaveSettings(settings))
                {
                    Log("更新了网站配置信息！");
                    return RedirectToSuccessPage("你已经成功更新了网站配置信息！");
                }
                return ErrorPage("更新网站信息配置错误！");
            }

            return Page();
        }

        /// <summary>
        /// 上传LOGO图片。
        /// </summary>
        /// <param name="file">上传文件实例。</param>
        /// <returns>返回上传结果。</returns>
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            var mediaDirectory = GetRequiredService<IMediaDirectory>();
            var result = await mediaDirectory.UploadAsync(file, "core");
            return Json(result);
        }
    }
}