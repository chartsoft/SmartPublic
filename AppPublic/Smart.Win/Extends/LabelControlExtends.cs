using DevExpress.XtraEditors;
using Smart.Win.Enums;

namespace Smart.Win.Extends
{
    /// <summary>
    /// 标签控件扩展
    /// </summary>
    public static class LabelControlExtends
    {
        /// <summary>
        /// 添加必填红星
        /// </summary>
        /// <param name="labelControl"></param>
        /// <param name="position"></param>
        public static void AddRequireRedStar(this LabelControl labelControl, PositionEnum position = PositionEnum.Left)
        {
            Smart.Win.UtilityHelper.AddRedStar(labelControl, position);
        }
    }
}
