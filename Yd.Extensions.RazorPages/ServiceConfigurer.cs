using Gentings;
using Gentings.AspNetCore.RazorPages;
using Gentings.Extensions.AspNetCore.EventLogging;
using Gentings.Extensions.Notifications;
using Gentings.Extensions.Settings;
using Gentings.Identity.Permissions;
using Gentings.Storages;
using Gentings.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yd.Extensions.Roles;

namespace Yd.Extensions.RazorPages
{
    /// <summary>
    /// 服务配置。
    /// </summary>
    [Suppress(typeof(Extensions.ServiceConfigurer))]
    public class ServiceConfigurer : Extensions.ServiceConfigurer
    {
        /// <summary>
        /// 配置服务。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        protected override void ConfigureIdentityServices(IServiceBuilder builder)
        {
            builder.AddResources<ServiceConfigurer>();
            base.ConfigureIdentityServices(builder);
            builder.AddServices(services =>
            {
                services.ConfigureApplicationCookie(options => Init(options, builder.Configuration.GetSection("User")))
                    .Configure<CookiePolicyOptions>(options =>
                    {
                        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                        options.CheckConsentNeeded = context => false; //是否开启GDPR
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });
            });
            builder.AddPermissions<Role, UserRole>()
                .AddNotification()
                .AddEventLoggers()
                .AddTaskServices()
                .AddMediaStorages()
                .AddSettings(true);
        }

        /// <summary>
        /// 配置Cookie验证实例。
        /// </summary>
        /// <param name="options">Cookie验证选项。</param>
        /// <param name="section">用户配置节点。</param>
        protected virtual void Init(CookieAuthenticationOptions options, IConfigurationSection section)
        {
            options.LoginPath = new PathString(section["Login"] ?? "/login");
            options.LogoutPath = new PathString(section["Logout"] ?? "/logout");
            options.AccessDeniedPath = new PathString(section["Denied"] ?? "/denied");
            options.ReturnUrlParameter = section["ReturnUrl"] ?? "returnUrl";
        }
    }
}