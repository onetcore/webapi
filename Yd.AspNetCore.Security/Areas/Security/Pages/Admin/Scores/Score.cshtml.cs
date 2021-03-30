using System.Threading.Tasks;
using Gentings.Security.Permissions;
using Gentings.Security.Scores;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Scores
{
    /// <summary>
    /// ���֡�
    /// </summary>
    [PermissionAuthorize(SecurityPermissions.Users)]
    public class ScoreModel : ModelBase
    {
        private readonly IUserManager _userManager;
        private readonly IScoreManager _scoreManager;
        /// <summary>
        /// ��ʼ����<see cref="ScoreModel"/>��
        /// </summary>
        /// <param name="userManager">�û�����ӿڡ�</param>
        /// <param name="scoreManager">���ֹ���ӿڡ�</param>
        public ScoreModel(IUserManager userManager, IScoreManager scoreManager)
        {
            _userManager = userManager;
            _scoreManager = scoreManager;
        }
        /// <summary>
        /// ��ǰ�û���
        /// </summary>
        public Extensions.Security.User CurrentUser { get; private set; }

        /// <summary>
        /// ��ǰ���֡�
        /// </summary>
        public int CurrentScore { get; private set; }

        /// <summary>
        /// ����ģ��ʵ����
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// �������á�
        /// </summary>
        public ScoreSettings ScoreSettings { get; private set; }

        /// <summary>
        /// ģ�����͡�
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// �û�Id��
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// ���֡�
            /// </summary>
            public int Score { get; set; }

            /// <summary>
            /// ��ע��
            /// </summary>
            public string Summary { get; set; }
        }

        /// <summary>
        /// ��ȡ�û�ʵ���Լ����֡�
        /// </summary>
        /// <param name="id">�û�Id��</param>
        /// <returns>���ص�ǰ��ͼ��</returns>
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
        /// ��ֵ��
        /// </summary>
        /// <returns>���س�ֵ�����</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var settings = await GetSettingsAsync<ScoreSettings>();
            if (Input.Score <= 0)
            {
                ModelState.AddModelError("Input.Score", $"��ֵ��{settings.ScoreName}����Ϊ������");
                return Error();
            }

            var result = await _scoreManager.RechargeAsync(Input.UserId, Input.Score, Input.Summary);
            if (result)
            {
                var user = await _userManager.FindByIdAsync(Input.UserId);
                await LogAsync($"�� {user.NickName} ��ֵ {Input.Score} {settings.ScoreUnit}����ע��{Input.Summary}");
                return Success("��ϲ�㣬���Ѿ��ɹ���ֵ�ɹ���");
            }
            return Error($"�ܱ�Ǹ����ֵʧ�ܣ�ԭ��{Localizer[result.Status]}��");
        }
    }
}
