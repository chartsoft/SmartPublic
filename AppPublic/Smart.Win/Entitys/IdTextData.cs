using System;

namespace Smart.Win.Entitys
{
    /// <summary>
    /// ID-Text-Data，用于Combo绑定
    /// </summary>
    public class IdTextData
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
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <returns></returns>
        public T GetData<T>() where T:class
        {
            return Data as T;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Text;
        }

        /// <summary>
        /// GetTypeCode
        /// </summary>
        /// <returns><see cref="TypeCode"/></returns>
        public TypeCode GetTypeCode()
        {
            return TypeCode.String;
        }

    }
    
}
