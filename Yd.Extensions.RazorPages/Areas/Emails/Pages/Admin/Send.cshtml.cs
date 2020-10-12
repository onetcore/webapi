﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Gentings.Extensions.Emails;
using Gentings.Identity.Permissions;
using Microsoft.AspNetCore.Mvc;

namespace Yd.Extensions.RazorPages.Areas.Emails.Pages.Admin
{
    /// <summary>
    /// 发送邮件。
    /// </summary>
    [PermissionAuthorize(EmailPermissions.Send)]
    public class SendModel : ModelBase
    {
        private readonly IEmailManager _messageManager;
        /// <summary>
        /// 初始化类<see cref="SendModel"/>。
        /// </summary>
        /// <param name="messageManager">电子邮件管理接口。</param>
        public SendModel(IEmailManager messageManager)
        {
            _messageManager = messageManager;
        }

        /// <summary>
        /// 邮件输入模型。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// 邮件输入模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// 邮件Id。
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 标题。
            /// </summary>
            [DisplayName("标题")]
            [Required(ErrorMessage = "{0}不能为空！")]
            public string Title { get; set; }

            /// <summary>
            /// 内容。
            /// </summary>
            [DisplayName("内容")]
            [Required(ErrorMessage = "{0}不能为空！")]
            public string Content { get; set; }

            /// <summary>
            /// 源代码。
            /// </summary>
            public string Source { get; set; }

            /// <summary>
            /// 邮件地址。
            /// </summary>
            [DisplayName("邮件地址")]
            [Required(ErrorMessage = "{0}不能为空！")]
            public string To { get; set; }
        }

        /// <summary>
        /// 获取邮件实例。
        /// </summary>
        /// <param name="id">邮件Id。</param>
        public void OnGet(int id)
        {
            var message = _messageManager.Find(id) ?? new Email();
            Input = new InputModel
            {
                Title = message.Title,
                Content = message.Content,
                To = message.To,
                Id = id,
                Source = message.Source ?? message.Content
            };
        }

        /// <summary>
        /// 保存邮件。
        /// </summary>
        /// <returns>返回保存结果。</returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Error();
            Email message;
            if (Input.Id > 0)
            {
                message = _messageManager.Find(Input.Id);
                if (message == null)
                    return Error("邮件不存在！");
                message.Title = Input.Title;
                message.Content = Input.Content;
                message.To = Input.To;
                message.Source = Input.Source;
                var hashKey = message.HashKey;
                message.HashKey = null;
                if (hashKey == message.HashKey || _messageManager.Update(Input.Id, new { Input.Title, Input.Content, message.ExtendProperties, Input.To, message.HashKey, Status = EmailStatus.Pending, TryTimes = 0 }))
                {
                    Notifier.Send(UserId, "邮件", "发送了一个电子邮件");
                    return Success("你已经成功发送邮件！");
                }
                return Error("发送邮件失败！");
            }

            message = new Email
            {
                UserId = UserId,
            };
            message.Title = Input.Title;
            message.Content = Input.Content;
            message.Source = Input.Source;
            message.To = Input.To;
            if (_messageManager.Save(message))
            {
                Notifier.Send(UserId, "邮件", "发送了一个电子邮件");
                return Success("你已经成功发送邮件！");
            }
            return Error("发送邮件失败！");
        }
    }
}