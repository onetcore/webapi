using Gentings.Identity.Permissions;

namespace Yd.Extensions
{
    /// <summary>
    /// 通用权限提供者。
    /// </summary>
    public class Permissions : PermissionProvider
    {
        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("admin", "初始配置管理", "允许用户可以管理初始配置相关内容!");
            Add("task", "后台服务", "允许管理后台服务相关操作!");
            Add("taskinterval", "配置定时模式", "允许配置后台服务定时模式相关操作!");
            Add("storages", "文件管理", "允许管理文件存储相关操作!");
        }

        /// <summary>
        /// 登录后台。
        /// </summary>
        public const string Administrator = "core.admin";

        /// <summary>
        /// 后台服务。
        /// </summary>
        public const string Task = "core.task";

        /// <summary>
        /// 配置后台服务定时模式。
        /// </summary>
        public const string TaskInterval = "core.taskinterval";

        /// <summary>
        /// 文件管理。
        /// </summary>
        public const string Storages = "core.storages";
    }
}