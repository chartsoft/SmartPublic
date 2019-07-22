using DevExpress.XtraEditors;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// SpinEdit控件辅助类
    /// </summary>
    public static class SpinEditHelper
    {
        /// <summary>
        /// 设置SpinEdit样式
        /// </summary>
        /// <param name="spin">spin控件</param>
        /// <param name="isFloat"></param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public static void SetSpinEditStyle(SpinEdit spin, bool isFloat, decimal minValue = 0, decimal maxValue = 0)
        {
            spin.Properties.MinValue = minValue;
            spin.Properties.IsFloatValue = false;
            spin.Properties.MaxValue = maxValue;
        }

        /// <summary>
        /// 设置SpinEdit样式
        /// </summary>
        /// <param name="spin">spin控件</param>
        /// <param name="decimalDigits">小数位数</param>
        /// <param name="minValue">最小值</param>
        /// <param name="maxValue">最大值</param>
        public static void SetSpinEditStyle(SpinEdit spin, int decimalDigits, decimal minValue = 0, decimal maxValue = 0)
        {
            spin.Properties.MinValue = minValue;
            spin.Properties.MaxValue = maxValue;
            spin.Properties.IsFloatValue = true;
            spin.Properties.Mask.EditMask = ("f" + decimalDigits);
            spin.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

    }
}