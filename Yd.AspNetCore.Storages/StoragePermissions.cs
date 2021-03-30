using Gentings.Security.Permissions;

namespace Yd.AspNetCore.Storages
{
    /// <summary>
    /// 存储提供者。
    /// </summary>
    public class StoragePermissions : PermissionProvider
    {
        /// <summary>
        /// 初始化权限实例。
        /// </summary>
        protected override void Init()
        {
            Add("storages", "文件管理", "允许管理文件存储相关操作!");
            Add("delstorages", "删除文件", "允许删除文件相关操作!");
            Add("createstorages", "上传文件", "允许上传文件相关操作!");
            Add("renamestorages", "重命名文件", "允许重命名文件相关操作!");
        }

        /// <summary>
        /// 文件管理。
        /// </summary>
        public const string View = "core.storages";

        /// <summary>
        /// 删除文件。
        /// </summary>
        public const string Delete = "core.delstorages";

        /// <summary>
        /// 上传文件。
        /// </summary>
        public const string Create = "core.createstorages";

        /// <summary>
        /// 重命名文件。
        /// </summary>
        public const string Rename = "core.renamestorages";
    }
}