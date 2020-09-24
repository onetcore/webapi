using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yd.Extensions.OpenServices
{
    /// <summary>
    /// 应用服务。
    /// </summary>
    [Table("open_Applications_Services")]
    public class ApplicationService
    {
        /// <summary>
        /// 应用Id。
        /// </summary>
        [Key]
        public Guid AppId { get; set; }

        /// <summary>
        /// 服务Id。
        /// </summary>
        [Key]
        public int ServiceId { get; set; }
    }
}