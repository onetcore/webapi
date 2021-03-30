using System.Threading.Tasks;
using Gentings.Security.Scores;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Security.Areas.Security.Pages.Admin.Scores
{
    /// <summary>
    /// �������á�
    /// </summary>
    public class SettingsModel : ModelBase
    {
        /// <summary>
        /// ��������ʵ����
        /// </summary>
        [BindProperty]
        public ScoreSettings Input { get; set; }

        /// <summary>
        /// ��ȡ�������á�
        /// </summary>
        public async Task OnGetAsync()
        {
            Input = await GetSettingsAsync<ScoreSettings>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Input.ScoreName))
            {
                ModelState.AddModelError("Input.ScoreName", "�������Ʋ���Ϊ�գ�");
                return Error();
            }

            var result = await SaveSettingsAsync(Input, "��������");
            if (result)
            {
                await LogAsync("�޸��˻������ã�");
                return Success("���Ѿ��ɹ������˻������ã�");
            }

            return Error("���»�������ʧ�ܣ������ԣ�");
        }
    }
}
