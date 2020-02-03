using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Gentings.Extensions;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户别名，用于推广。
    /// </summary>
    [Table("core_Users_Alias")]
    public class UserAlias : IIdObject<Guid>
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 唯一Id。
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 推广数量。
        /// </summary>
        public int Count { get; set; }
    }
}