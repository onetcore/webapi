namespace Yd.WebUI.Core
{
    /// <summary>
    /// 分页数据结果。
    /// </summary>
    /// <typeparam name="TData">查询实例。</typeparam>
    public class ApiPageResult<TData> : ApiResult
    {
        /// <summary>
        /// 页码。
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 每页显示记录数。
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数。
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 总页数。
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// 数据实例。
        /// </summary>
        public IPageEnumerable<TData> Data { get; set; }
    }
}