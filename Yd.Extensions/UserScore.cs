using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gentings;
using Gentings.Extensions;

namespace Yd.Extensions
{
    /// <summary>
    /// 用户积分日志。
    /// </summary>
    [Table("core_Users_Scores")]
    public class UserScore : IIdObject<int>
    {
        /// <summary>
        /// 获取或设置唯一Id。
        /// </summary>
        [Identity]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id。
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 发生改变的积分。
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 原始积分。
        /// </summary>
        public decimal BeforeScore { get; set; }

        /// <summary>
        /// 改变后剩余的积分。
        /// </summary>
        public decimal AfterScore { get; set; }

        /// <summary>
        /// 添加时间。
        /// </summary>
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        private string _securityKey;
        /// <summary>
        /// 安全码。
        /// </summary>
        [Size(36)]
        public string SecurityKey
        {
            get => _securityKey ??= HashedKey;
            set => _securityKey = value;
        }

        /// <summary>
        /// 哈希码。
        /// </summary>
        protected virtual string HashedKey => Cores.Md5(Cores.Sha1(
                                                        $"{UserId}:{Score:C}:{BeforeScore:C}:{AfterScore:C}:{CreatedDate:yyyy-MM-DD HH:mm:ss}") + $"{UserId}:{CreatedDate:yyyy-MM-DD HH:mm:ss}");

        /// <summary>
        /// 是否合法。
        /// </summary>
        public bool IsValid => SecurityKey.Equals(HashedKey, StringComparison.OrdinalIgnoreCase);
    }
}