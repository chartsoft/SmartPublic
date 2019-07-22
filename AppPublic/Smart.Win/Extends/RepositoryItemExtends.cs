using DevExpress.XtraEditors.Repository;

namespace Smart.Win.Extends
{
    /// <summary>
    /// XtraGrid辅助类
    /// </summary>
    public static partial class RepositoryItemExtends
    {

        /// <summary>
        /// 自动高度
        /// </summary>
        /// <param name="memoItem">表格控件</param>
        /// <param name="autoHeight">是否自动行高度，默认true</param>
        public static void AutoHeight(this RepositoryItemMemoEdit memoItem, bool autoHeight = true)
        {
            memoItem.LinesCount = autoHeight ? 0 : 1;
        }

        /// <summary>
        /// 自动高度
        /// </summary>
        /// <param name="pictureItem">表格控件</param>
        public static void AutoHeight(this RepositoryItemPictureEdit pictureItem)
        {
            pictureItem.CustomHeight = 0;
        }

        /// <summary>
        /// 自动高度
        /// </summary>
        /// <param name="pictureItem">表格控件</param>
        /// <param name="height">图片高度</param>
        public static void PictureHeight(this RepositoryItemPictureEdit pictureItem,int height)
        {
            pictureItem.CustomHeight = height;
        }

    }
}
