using System.Threading.Tasks;
using Gentings.Security.Permissions;
using Gentings.Security.Scores;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Scores
{
    /// <summary>
    /// 积分。
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Users)]
    public class ScoreModel : ModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IScoreManager _scoreManager;
        /// <summary>
        /// 初始化类<see cref="ScoreModel"/>。
        /// </summary>
        /// <param name="userManager">用户管理接口。</param>
        /// <param name="scoreManager">积分管理接口。</param>
        public ScoreModel(IUserManager userManager, IScoreManager scoreManager)
        {
            _userManager = userManager;
            _scoreManager = scoreManager;
        }
        /// <summary>
        /// 当前用户。
        /// </summary>
        public Extensions.Security.User CurrentUser { get; private set; }

        /// <summary>
        /// 当前积分。
        /// </summary>
        public int CurrentScore { get; private set; }

        /// <summary>
        /// 输入模型实例。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// 积分配置。
        /// </summary>
        public ScoreSettings ScoreSettings { get; private set; }

        /// <summary>
        /// 模型类型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// 用户Id。
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 积分。
            /// </summary>
            public int Score { get; set; }

            /// <summary>
            /// 备注。
            /// </summary>
            public string Summary { get; set; }
        }

        /// <summary>
        /// 获取用户实例以及积分。
        /// </summary>
        /// <param name="id">用户Id。</param>
        /// <returns>返回当前视图。</returns>
        public async Task<IActionResult> OnGetAsync(int id)
        {
            CurrentUser = await _userManager.FindByIdAsync(id);
            if (CurrentUser == null)
                return NotFound();
            CurrentScore = await _scoreManager.GetScoreAsync(id);
            ScoreSettings = await GetSettingsAsync<ScoreSettings>();
            Input = new InputModel { UserId = id, Score = 1000 };
            return Page();
        }

        /// <summary>
        /// 充值。
        /// </summary>
        /// <returns>返回充值结果。</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var settings = await GetSettingsAsync<ScoreSettings>();
            if (Input.Score <= 0)
            {
                ModelState.AddModelError("Input.Score", $"充值的{settings.ScoreName}不能为复数！");
                return Error();
            }

            var result = await _scoreManager.RechargeAsync(Input.UserId, Input.Score, Input.Summary);
            if (result)
            {
                var user = await _userManager.FindByIdAsync(Input.UserId);
                await LogAsync($"对 {user.NickName} 充值 {Input.Score} {settings.ScoreUnit}，备注：{Input.Summary}");
                return Success("恭喜你，你已经成功充值成功！");
            }
            return Error($"很抱歉，充值失败，原因：{Localizer[result.Status]}。");
        }
    }
}
