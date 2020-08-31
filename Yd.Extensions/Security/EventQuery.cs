using Gentings.Data;
using Gentings.Extensions.AspNetCore.EventLogging;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 事件查询实例。
    /// </summary>
    public class EventQuery : EventQueryBase<User>
    {
        /// <summary>
        /// 当前用户角色等级。
        /// </summary>
        public int RoleLevel { get; set; }

        /// <summary>
        /// 初始化查询上下文。
        /// </summary>
        /// <param name="context">查询上下文。</param>
        protected override void Init(IQueryContext<EventMessage> context)
        {
            base.Init(context);
            if (RoleLevel > 0)
            {
                context.Select()
                    .LeftJoin<User, Role>((u, r) => u.RoleId == r.Id)
                    .Where<Role>(x => x.RoleLevel <= RoleLevel);
            }
        }
    }
}