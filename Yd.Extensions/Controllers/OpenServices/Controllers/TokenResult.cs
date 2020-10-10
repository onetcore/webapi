using Gentings.AspNetCore;

namespace Yd.Extensions.Controllers.OpenServices.Controllers
{
    /// <summary>
    /// 输出模型。
    /// </summary>
    public class TokenResult : ApiDataResult<string>
    {
        /// <summary>
        /// 初始化类<see cref="TokenResult"/>。
        /// </summary>
        /// <param name="data">数据实例。</param>
        public TokenResult(string data) : base(data)
        {
        }
    }
}