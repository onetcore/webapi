using System.Linq;
using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Extensions.Settings;
using Gentings.Security;
using Gentings.Storages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.AspNetCore
{
    /// <summary>
    /// 页面模型基类。
    /// </summary>
    public abstract class ModelBase : Gentings.AspNetCore.ModelBase
    {
        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        public new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        public Role Role => _role ??= GetRequiredService<IRoleManager>().GetCacheRole(User.RoleId);

        private IUserManager _userManager;
        /// <summary>
        /// 用户管理接口实例。
        /// </summary>
        public IUserManager UserManager => _userManager ??= GetRequiredService<IUserManager>();

        /// <summary>
        /// 获取缓存用户。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <returns>返回缓存用户实例。</returns>
        public CachedUser GetUser(int userId) => UserManager.GetCachedUser(userId);

        /// <summary>
        /// 获取用户昵称。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <returns>返回缓存用户昵称。</returns>
        public string GetNickName(int userId) => UserManager.GetCachedUser(userId)?.NickName;

        /// <summary>
        /// 获取用户角色名称。
        /// </summary>
        /// <param name="userId">用户Id。</param>
        /// <returns>返回缓存角色名称。</returns>
        public string GetRoleName(int userId) => UserManager.GetCachedUser(userId)?.RoleName;

        /// <summary>
        /// 保存配置实例。
        /// </summary>
        /// <param name="settings">当前配置实例对象。</param>
        /// <param name="name">配置名称。</param>
        /// <returns>返回配置结果。</returns>
        protected async Task<bool> SaveSettingsAsync(object settings, string name)
        {
            var result = await GetRequiredService<ISettingsManager>().SaveSettingsAsync(settings);
            if (result)
                await LogAsync(Localizer.GetString(DataAction.Updated, name));
            return result;
        }

        /// <summary>
        /// 获取配置实例。
        /// </summary>
        /// <typeparam name="TSettings">配置类型。</typeparam>
        /// <returns>返回配置实例结果。</returns>
        protected Task<TSettings> GetSettingsAsync<TSettings>()
            where TSettings : class, new()
        {
            return GetRequiredService<ISettingsManager>().GetSettingsAsync<TSettings>();
        }

        private SiteSettings _siteSettings;
        /// <summary>
        /// 网站配置实例。
        /// </summary>
        public SiteSettings SiteSettings => _siteSettings ??= GetRequiredService<SiteSettings>();

        private SecuritySettings _settings;
        /// <summary>
        /// 安全配置。
        /// </summary>
        public SecuritySettings Settings => _settings ??= GetRequiredService<SecuritySettings>();

        /// <summary>
        /// 返回JSON试图结果。
        /// </summary>
        /// <param name="result">数据结果。</param>
        /// <returns>返回JSON试图结果。</returns>
        protected IActionResult Error(IdentityResult result)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return Error(string.Join(", ", errors));
        }

        /// <summary>
        /// 返回JSON试图结果。
        /// </summary>
        /// <param name="result">数据结果。</param>
        /// <returns>返回JSON试图结果。</returns>
        protected IActionResult Json(MediaResult result)
        {
            if (result.Succeeded)
                return Success(new { result.Url });
            return Error(result.Message);
        }

        private INamedStringManager _namedStringManager;
        private INamedStringManager NamedStringManager =>
            _namedStringManager ??= GetRequiredService<INamedStringManager>();

        /// <summary>
        /// 获取字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        public string GetNamedString(string path) => NamedStringManager.GetString(path);

        /// <summary>
        /// 获取或添加字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        public string GetOrAddNamedString(string path) => NamedStringManager.GetOrAddString(path);

        /// <summary>
        /// 获取字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        public Task<string> GetNamedStringAsync(string path) => NamedStringManager.GetStringAsync(path);

        /// <summary>
        /// 获取或添加字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        public Task<string> GetOrAddNamedStringAsync(string path) => NamedStringManager.GetOrAddStringAsync(path);
    }
}