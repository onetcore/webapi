using System.ComponentModel.DataAnnotations.Schema;
using Gentings.Identity;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户。
    /// </summary>
    public class User : UserBase
    {
        /// <summary>
        /// 积分。
        /// </summary>
        [NotMapped]
        public virtual int Score { get; set; }
    }
}