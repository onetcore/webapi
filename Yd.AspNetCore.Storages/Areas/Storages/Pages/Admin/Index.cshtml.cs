﻿using System;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Storages.Areas.Storages.Pages.Admin
{
    public class IndexModel : AdminModelBase
    {
        private readonly IMediaDirectory _mediaDirectory;

        public IndexModel(IMediaDirectory mediaDirectory)
        {
            _mediaDirectory = mediaDirectory;
        }

        [BindProperty(SupportsGet = true)]
        public MediaQuery Query { get; set; }

        public IPageEnumerable<MediaFile> Files { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Files = await _mediaDirectory.LoadAsync(Query);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid[] ids)
        {
            if (ids == null || ids.Length == 0)
                return Error("请选择文件后再进行删除操作！");
            foreach (var id in ids)
            {
                await _mediaDirectory.DeleteAsync(id);
            }
            return Success("你已经成功删除文件！");
        }

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {
            if (file?.Length <= 0)
                return Error("请选择非空文件后，再上传！");
            var result = await _mediaDirectory.UploadAsync(file, "core");
            return Success("你已经成功上传了文件！", result);
        }
    }
}