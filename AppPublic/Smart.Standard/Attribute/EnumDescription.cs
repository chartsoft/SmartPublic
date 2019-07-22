using System;

namespace Smart.Standard.Attribute
{
    /// <summary>
    /// 把枚举值按照指定的文本显示
    /// <example>
    /// EnumDescription.GetEnumText(typeof(MyEnum));
    /// EnumDescription.GetFieldText(MyEnum.EnumField);
    /// EnumDescription.GetFieldInfos(typeof(MyEnum));  
    /// </example>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
    public partial class EnumDescription : System.Attribute
    {
        /// <summary>
        /// 描述枚举值
        /// </summary>
        /// <param name="enumDisplayText">描述内容</param>
        /// <param name="constValueText">常量值</param>
        public EnumDescription(string enumDisplayText,string constValueText = null)
        {
            EnumDisplayText = enumDisplayText;
            ConstValue = constValueText;
        }

        /// <summary>
        /// 枚举显示文本
        /// </summary>
        public string EnumDisplayText { get;}

        /// <summary>
        /// 枚举值
        /// </summary>
        public int EnumValue { get; private set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// ConstValue
        /// </summary>
        public string ConstValue { get; }
        /// <summary>
        ///  重写ToString
        /// </summary>
        /// <returns>EnumDisplayText</returns>
        public override string ToString()
        {
            return EnumDisplayText;
        }

    }
}