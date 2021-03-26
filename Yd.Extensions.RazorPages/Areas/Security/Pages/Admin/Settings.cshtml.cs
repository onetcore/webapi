using System.Threading.Tasks;
using Gentings.Extensions.Settings;
using Gentings.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.Extensions.RazorPages.Areas.Security.Pages.Admin
{
    /// <summary>
    /// 用户配置。
    /// </summary>
    public class SettingsModel : ModelBase
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
        /// 用户配置实例。
        /// </summary>
        [BindProperty]
        public SecuritySettings Input { get; set; }

        /// <summary>
        /// 获取当前网站配置。
        /// </summary>
        public void OnGet()
        {
            Input = _settingsManager.GetSettings<SecuritySettings>();
        }

        /// <summary>
        /// 保存网站配置。
        /// </summary>
        /// <returns>返回保存结果。</returns>
        public IActionResult OnPost()
        {
            var settings = _settingsManager.GetSettings<SecuritySettings>();
            var differ = GetObjectDiffer(settings);
            if (differ.IsDifference(Input))
            {
                _settingsManager.SaveSettings(Input);
                Log($"更新了用户配置信息：{differ}");
            }

            return SuccessPage("你已经成功更新了配置！");
        }

        /// <summary>
        /// 上传登录页面背景图片。
        /// </summary>
        /// <param name="file">上传文件实例。</param>
        /// <returns>返回上传结果。</returns>
        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            var mediaDirectory = GetRequiredService<IMediaDirectory>();
            var result = await mediaDirectory.UploadAsync(file, SecuritySettings.ExtensionName);
            return Json(result);
        }
    }
}