using Gentings.Extensions;
using Gentings.Extensions.Emails;
using Microsoft.AspNetCore.Mvc;

namespace Yd.AspNetCore.Emails.Areas.Emails.Pages.Admin
{
    /// <summary>
    /// 邮件发送列表。
    /// </summary>
    public class IndexModel : AdminModelBase
    {
        private readonly IEmailManager _messageManager;
        /// <summary>
        /// 初始化类<see cref="IndexModel"/>。
        /// </summary>
        /// <param name="messageManager">电子邮件管理接口。</param>
        public IndexModel(IEmailManager messageManager)
        {
            _messageManager = messageManager;
        }

        /// <summary>
        /// 邮件查询实例。
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public EmailQuery Query { get; set; }

        /// <summary>
        /// 邮件列表。
        /// </summary>
        public IPageEnumerable<Email> Emails { get; private set; }

        /// <summary>
        /// 获取邮件列表。
        /// </summary>
        public void OnGet()
        {
            Emails = _messageManager.Load(Query);
        }
    }
}