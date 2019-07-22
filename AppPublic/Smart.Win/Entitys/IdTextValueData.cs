namespace Smart.Win.Entitys
{
    /// <summary>
    /// ID-Text-Data，用于Combo绑定
    /// </summary>
    public class IdTextValueData<T> 
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 显示文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

    }
}
