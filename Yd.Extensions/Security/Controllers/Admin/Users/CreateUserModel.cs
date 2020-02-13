using System.ComponentModel.DataAnnotations;

namespace Yd.Extensions.Security.Controllers.Admin.Users
{
    /// <summary>
    /// 新建用户模型。
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// 电话号码。
        /// </summary>
        [StringLength(11, MinimumLength = 11, ErrorMessage = "手机号码错误")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 电子邮件。
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 真实姓名。
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户名。
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空！")]
        [RegularExpression("^[a-zA-Z][a-z0-9A-Z]{4,11}$", ErrorMessage = "英文开头，由数字和英文字母组成的5-12个字符！")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [RegularExpression(".{6,16}", ErrorMessage = "密码由6-16个字符组成！")]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        [Compare(nameof(Password), ErrorMessage = "确认密码和密码不匹配！")]
        public string Confirm { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Summary { get; set; }
    }

    /// <summary>
    /// 更新用户模型。
    /// </summary>
    public class UpdateUserModel : CreateUserModel
    {
        /// <summary>
        /// 用户Id。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 是否激活锁定功能。
        /// </summary>
        public bool LockoutEnabled { get; set; }
    }
}