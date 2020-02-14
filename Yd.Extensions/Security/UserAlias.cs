using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gentings;
using Gentings.Extensions;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户别名，用于推广。
    /// </summary>
    [Table("core_Users_Alias")]
    public class UserAlias : IIdObject<string>
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 唯一Id。
        /// </summary>
        [Key]
        [Size(16)]
        public string Id { get; set; } = Cores.NewId();

        /// <summary>
        /// 用户等级。
        /// </summary>
        [NotMapped]
        public int Level { get; set; }

        /// <summary>
        /// 推广数量。
        /// </summary>
        public int Count { get; set; }
    }
}