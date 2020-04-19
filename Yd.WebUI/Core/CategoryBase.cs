namespace Yd.WebUI.Core
{
    /// <summary>
    /// 分类基类。
    /// </summary>
    public abstract class CategoryBase
    {
        /// <summary>
        /// 获取或设置唯一Id。
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 分类名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}