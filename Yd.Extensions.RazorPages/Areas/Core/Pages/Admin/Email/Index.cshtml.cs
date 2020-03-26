using Gentings.Extensions;
using Gentings.Identity.Permissions;
using Gentings.Messages.Emails;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Core.Pages.Admin.Email
{
    [PermissionAuthorize(Permissions.Email)]
    public class IndexModel : ModelBase
    {
        private readonly IEmailManager _messageManager;

        public IndexModel(IEmailManager messageManager)
        {
            _messageManager = messageManager;
        }

        [BindProperty(SupportsGet = true)]
        public EmailQuery Query { get; set; }

        public IPageEnumerable<Gentings.Messages.Emails.Email> Emails { get; private set; }

        public void OnGet()
        {
            Emails = _messageManager.Load(Query);
        }
    }
}