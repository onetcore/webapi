using System.Threading.Tasks;
using Gentings.Extensions;
using Gentings.Extensions.Settings;
using Gentings.Security;
using Microsoft.AspNetCore.Mvc;
using Yd.Extensions.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions
{
    /// <summary>
    /// 控制器基类。
    /// </summary>
    public abstract class ControllerBase : Gentings.AspNetCore.ControllerBase
    {
        private User _user;
        /// <summary>
        /// 当前登录用户Id。
        /// </summary>
        protected new User User => _user ??= HttpContext.GetUser<User>();

        private Role _role;
        /// <summary>
        /// 当前用户的最大角色实例。
        /// </summary>
        protected Role Role => _role ??= GetRequiredService<IRoleManager>().FindById(User.RoleId);

        private SiteSettings _siteSettings;
        /// <summary>
        /// 网站配置实例。
        /// </summary>
        protected SiteSettings SiteSettings => _siteSettings ??= GetRequiredService<SiteSettings>();

        /// <summary>
        /// 保存配置实例。
        /// </summary>
        /// <param name="settings">当前配置实例对象。</param>
        /// <param name="name">配置名称。</param>
        /// <returns>返回配置结果。</returns>
        protected virtual async Task<IActionResult> SaveSettingsAsync(object settings, string name)
        {
            var result = await GetRequiredService<ISettingsManager>().SaveSettingsAsync(settings);
            return await LogDataResultAsync(result, DataAction.Updated, name);
        }

        /// <summary>
        /// 获取配置实例。
        /// </summary>
        /// <typeparam name="TSettings">配置类型。</typeparam>
        /// <returns>返回配置实例结果。</returns>
        protected async Task<IActionResult> GetSettingsAsync<TSettings>()
            where TSettings : class, new()
        {
            var result = await GetRequiredService<ISettingsManager>().GetSettingsAsync<TSettings>();
            return OkResult(result);
        }

        private INamedStringManager _namedStringManager;
        private INamedStringManager NamedStringManager =>
            _namedStringManager ??= GetRequiredService<INamedStringManager>();

        /// <summary>
        /// 获取字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        protected string GetNamedString(string path) => NamedStringManager.GetString(path);

        /// <summary>
        /// 获取或添加字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        protected string GetOrAddNamedString(string path) => NamedStringManager.GetOrAddString(path);

        /// <summary>
        /// 获取字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        protected Task<string> GetNamedStringAsync(string path) => NamedStringManager.GetStringAsync(path);

        /// <summary>
        /// 获取或添加字典字符串。
        /// </summary>
        /// <param name="path">字典路径实例。</param>
        /// <returns>返回当前字典字符串。</returns>
        protected Task<string> GetOrAddNamedStringAsync(string path) => NamedStringManager.GetOrAddStringAsync(path);
    }
}