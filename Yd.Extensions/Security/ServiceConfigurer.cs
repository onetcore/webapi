﻿using System;
using Gentings;
using Gentings.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 注册当前登录用户。
    /// </summary>
    public class ServiceConfigurer : ServiceConfigurer<User, Role, UserStore, RoleStore, UserManager, RoleManager>
    {
        /// <summary>
        /// 配置服务。
        /// </summary>
        /// <param name="builder">容器构建实例。</param>
        protected override void ConfigureIdentityServices(IServiceBuilder builder)
        {
            builder.AddServices(services =>
            {
                services.Configure<IdentityOptions>(options =>
                    {
                        //锁定配置
                        options.Lockout = new LockoutOptions
                        {
                            AllowedForNewUsers = true,
                            DefaultLockoutTimeSpan = TimeSpan.FromHours(1),
                            MaxFailedAccessAttempts = 5
                        };
                        //用户配置
                        options.User = new UserOptions
                        {
                            AllowedUserNameCharacters = null,
                            RequireUniqueEmail = false
                        };
                        //需要激活电子邮件
                        options.SignIn.RequireConfirmedEmail = false;
                    })
                    .AddScoped(service => service.GetRequiredService<IUserManager>().GetUser() ?? _anonymous);
            });
        }

        private static readonly User _anonymous = new User { UserName = "Anonymous" };
    }
}