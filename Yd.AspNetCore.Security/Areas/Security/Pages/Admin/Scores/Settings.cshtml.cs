using System.Threading.Tasks;
using Gentings.Security.Scores;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Scores
{
    /// <summary>
    /// 积分配置。
    /// </summary>
    public class SettingsModel : ModelBase
    {
        /// <summary>
        /// 积分配置实例。
        /// </summary>
        [BindProperty]
        public ScoreSettings Input { get; set; }

        /// <summary>
        /// 获取积分配置。
        /// </summary>
        public async Task OnGetAsync()
        {
            Input = await GetSettingsAsync<ScoreSettings>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.ScoreName))
            {
                ModelState.AddModelError("Input.ScoreName", "积分名称不能为空！");
                return Error();
            }

            var result = await SaveSettingsAsync(Input, "积分配置");
            if (result)
            {
                await LogAsync("修改了积分配置！");
                return Success("你已经成功更新了积分配置！");
            }

            return Error("更新积分配置失败，请重试！");
        }
    }
}
