using Gentings.Identity;
using Gentings.Messages.Notifications;
using Yd.Extensions.Roles;

namespace Yd.Extensions.RazorPages
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : Gentings.AspNetCore.RazorPages.ModelBase
    {
        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        public new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        public Role Role => _role ??= GetRequiredService<IRoleManager>().GetCacheRole(User.RoleId);

        private INotifier _notifier;
        /// <summary>
        /// 通知信息。
        /// </summary>
        protected INotifier Notifier => _notifier ??= GetRequiredService<INotifier>();
    }
}