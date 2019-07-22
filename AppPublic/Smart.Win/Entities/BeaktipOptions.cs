using System;
using DevExpress.Utils;
using Smart.Win.Enums;

namespace Smart.Win.Entities
{
    /// <summary>
    /// Beaktip选项
    /// </summary>
    public class BeaktipOptions : BeakPanelOptions
    {
        /// <summary>
        /// Beaktip选项
        /// </summary>
        public BeaktipOptions() : this(new FlyoutPanel()) { }

        /// <summary>
        /// Tip显示位置
        /// <para>Tip显示位置和Beak显示方向正好想法</para>
        /// <para>需要为显示控件预留足够的显示空间，否则控件会自动计算显示方向</para>
        /// </summary>
        public PositionEnum TipPosition
        {
            get => BeakPosition2TipPosition(BeakLocation);
            set => BeakLocation = TipPosition2BeakPosition(value);
        }
        /// <summary>
        /// Beaktip选项
        /// </summary>
        /// <param name="fPanel">要显示的<see cref="FlyoutPanel"/>控件</param>
        private BeaktipOptions(FlyoutPanel fPanel) : base(fPanel) { }
        /// <summary>
        /// 要显示的<see cref="FlyoutPanel"/>控件
        /// </summary>
        public FlyoutPanel TipPanel => Owner;

        /// <summary>
        /// 重写ToString
        /// </summary>
        public override string ToString()
        {
            return typeof(BeaktipOptions).FullName ?? throw new InvalidOperationException();
        }

        #region [Supports]
        
        /// <summary>
        /// BeakPosition转TipPosition
        /// </summary>
        private PositionEnum BeakPosition2TipPosition(BeakPanelBeakLocation beakLocation)
        {
            switch (beakLocation)
            {
                case BeakPanelBeakLocation.Top:
                    return PositionEnum.Bottom;
                case BeakPanelBeakLocation.Bottom:
                    return PositionEnum.Top;
                case BeakPanelBeakLocation.Left:
                    return PositionEnum.Right;
                case BeakPanelBeakLocation.Right:
                    return PositionEnum.Left;
                default:
                    return PositionEnum.Left;
            }
        }

        /// <summary>
        /// TipPosition转BeakPosition
        /// </summary>
        private BeakPanelBeakLocation TipPosition2BeakPosition(PositionEnum tipLocation)
        {
            switch (tipLocation)
            {
                case PositionEnum.Top:
                    return BeakPanelBeakLocation.Bottom;
                case PositionEnum.Bottom:
                    return BeakPanelBeakLocation.Top;
                case PositionEnum.Left:
                    return BeakPanelBeakLocation.Right;
                case PositionEnum.Right:
                    return BeakPanelBeakLocation.Left;
                default:
                    return BeakPanelBeakLocation.Left;
            }
        }

        #endregion

    }
}
