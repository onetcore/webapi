﻿using Gentings;
using Gentings.Security;
using Yd.Extensions.Security.Roles;

namespace Yd.Extensions.Security
{
    /// <summary>
    /// 用户管理接口。
    /// </summary>
    public interface IUserManager : IUserManager<User, Role>, IScopedService
    {
    }
}