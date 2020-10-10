using System.ComponentModel.DataAnnotations;

namespace Yd.Extensions.Controllers.OpenServices.Controllers
{
    /// <summary>
    /// Token模型。
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// AppId。
        /// </summary>
        [Required(ErrorMessage = "AppId不能为空！")]
        public string AppId { get; set; }

        /// <summary>
        /// 密钥。
        /// </summary>
        [Required(ErrorMessage = "密钥不能为空！")]
        public string AppSecret { get; set; }
    }
}