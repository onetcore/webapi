using Gentings.Data;
using Gentings.Extensions.Events;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 事件查询实例。
    /// </summary>
    public class EventQuery : Gentings.Security.EventQuery<User>
    {
        /// <summary>
        /// 用户。
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 初始化用户条件。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void InitUsers(IQueryContext<Event> context)
        {
            base.InitUsers(context);
            if (!string.IsNullOrEmpty(User))
                context.InnerJoin<User>((e, u) => e.UserId == u.Id)
                    .Where<User>(x => x.NormalizedUserName.Contains(User) || x.NickName.Contains(User));
        }
    }
}