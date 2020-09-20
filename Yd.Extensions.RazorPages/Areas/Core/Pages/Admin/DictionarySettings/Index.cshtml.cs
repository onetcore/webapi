﻿using System.Linq;
using Gentings.Extensions.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin.DictionarySettings
{
    public class IndexModel : ModelBase
    {
        private readonly ISettingDictionaryManager _settingManager;
        public IndexModel(ISettingDictionaryManager settingManager)
        {
            _settingManager = settingManager;
        }

        public SettingDictionary Current { get; private set; }

        public void OnGet(int id = 0)
        {
            Current = _settingManager.Find(id);
        }

        public IActionResult OnPostDelete(int[] ids, int pid)
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择实例后再进行删除操作！");
            var settings = _settingManager.Find(pid).Children.Where(x => ids.Contains(x.Id)).ToList();
            foreach (var setting in settings)
            {
                if (setting.Count > 0)
                    return Error($"{setting.Value} 下面的字典实例不为空，需要先清空子项，才能进行删除操作！");
            }

            var result = _settingManager.Delete(ids);
            if (result)
            {
                Log("删除了字典实例：{0}", string.Join(",", settings.Select(x => x.Value)));
            }

            return Json(result, "字典实例");
        }
    }
}