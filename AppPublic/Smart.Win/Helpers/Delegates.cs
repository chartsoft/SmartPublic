using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using Smart.Net45.Interface;
using Smart.Win.Supports;

namespace Smart.Win.Helpers
{

    /// <summary>
    /// 有 HitInfo 和 模型两个参数的谓词委托
    /// </summary>
    /// <typeparam name="T">模型类型</typeparam>
    /// <param name="model">模型</param>
    /// <param name="columnView"><see cref="ColumnView"/>当前View</param>
    /// <param name="hitInfo"><see cref="BaseHitInfo"/>信息</param>
    /// <returns>是否通过</returns>
    public delegate bool BaseHitInfoModelPredicate<T>(ColumnView columnView,BaseHitInfo hitInfo, T model) where T : class, IKey;

}
