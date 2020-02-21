using Yd.Extensions.Roles;

namespace Yd.Extensions.Controllers.Admin.Roles
{
    /// <summary>
    /// 添加角色模型。
    /// </summary>
    public class CreateRoleModel
    {
        /// <summary>
        /// 角色名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 颜色。
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 系统管理员。
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 默认角色。
        /// </summary>
        public bool IsDefault { get; set; }
    }

    /// <summary>
    /// 更新角色模型。
    /// </summary>
    public class UpdateRoleModel : CreateRoleModel
    {
        /// <summary>
        /// 角色Id。
        /// </summary>
        public int Id { get; set; }
    }

    /// <summary>
    /// 角色模型。
    /// </summary>
    public class RoleModel : UpdateRoleModel
    {
        internal RoleModel(Role role)
        {
            Id = role.Id;
            Color = role.Color;
            IconUrl = role.IconUrl;
            Name = role.Name;
            Level = role.RoleLevel;
            IsSystem = role.IsSystem;
            IsDefault = role.IsDefault;
        }

        /// <summary>
        /// 角色等级。
        /// </summary>
        public int Level { get; }
    }
}