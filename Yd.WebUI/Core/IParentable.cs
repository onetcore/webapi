using System.Collections.Generic;

namespace Yd.WebUI.Core
{
    /// <summary>
    /// 递归接口。
    /// </summary>
    public interface IParentable
    {
        /// <summary>
        /// 唯一Id。
        /// </summary>
        int Id { get; }

        /// <summary>
        /// 父级Id。
        /// </summary>
        int ParentId { get; }

        /// <summary>
        /// 父级实例。
        /// </summary>
        object Parent { get; }

        /// <summary>
        /// 获取子项。
        /// </summary>
        List<object> Children { get; }

        /// <summary>
        /// 层次等级。
        /// </summary>
        int Level { get; }

        /// <summary>
        /// 包含子项数量。
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// 递归接口。
    /// </summary>
    public interface IParentable<TModel> : IParentable
        where TModel : IParentable<TModel>
    {
        /// <summary>
        /// 父级实例。
        /// </summary>
        new TModel Parent { get; }

        /// <summary>
        /// 获取子项。
        /// </summary>
        new List<TModel> Children { get; }

        /// <summary>
        /// 添加子集实例。
        /// </summary>
        /// <param name="model">子集实例。</param>
        void Add(TModel model);
    }
}