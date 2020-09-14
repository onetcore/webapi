using System;
using System.Linq;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Extensions.SensitiveWords;
using Gentings.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin.SensitiveWords
{
    public class IndexModel : ModelBase
    {
        private readonly ISensitiveWordManager _sensitiveWordManager;
        private readonly IStorageDirectory _storageDirectory;

        public IndexModel(ISensitiveWordManager sensitiveWordManager, IStorageDirectory storageDirectory)
        {
            _sensitiveWordManager = sensitiveWordManager;
            _storageDirectory = storageDirectory;
        }

        [BindProperty(SupportsGet = true)]
        public SensitiveWordQuery Query { get; set; }

        public IPageEnumerable<SensitiveWord> Words { get; set; }

        public void OnGet()
        {
            Words = _sensitiveWordManager.Load(Query);
        }

        public IActionResult OnPostDelete(int[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择实例后再进行删除操作！");
            var result = _sensitiveWordManager.Delete(ids);
            return Json(result, "敏感词汇");
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            if (file.Length == 0)
                return Error("请选择有内容的文件后再上传！");
            var tempFile = await _storageDirectory.SaveToTempAsync(file);
            var text = await StorageUtility.ReadTextAsync(tempFile.FullName);
            if (string.IsNullOrWhiteSpace(text))
                return Error("文件内容为空字符串，请重新上传！");
            var words = text.Trim().Split(new string[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length < 32)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
            var result = await _sensitiveWordManager.ImportAsync(words);
            if (result)
                return Success($"恭喜你已经成功上传了{words.Count}个敏感词汇！");
            return Error("上传敏感词汇失败，请重试！");
        }
    }
}