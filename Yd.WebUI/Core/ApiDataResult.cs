﻿namespace Yd.WebUI.Core
{
    /// <summary>
    /// 包含数据的结果。
    /// </summary>
    /// <typeparam name="TData">数据类型。</typeparam>
    public class ApiDataResult<TData> : ApiResult
    {
        /// <summary>
        /// 数据实例。
        /// </summary>
        public TData Data { get; set; }
    }
}