using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 子用户索引表格。
    /// </summary>
    [Table("core_Users_Subusers")]
    public class Subuser
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// 子用户Id。
        /// </summary>
        [Key]
        public int SubId { get; set; }
    }
}