using Smart.Net45.Attribute;
using Smart.Net45.Consts;

namespace Smart.Net45.Enum
{
    /// <summary>
    /// 正则匹配枚举
    /// </summary>
    [EnumDescription("正则匹配枚举")]
    public enum RegexKinds
    {
        /// <summary>
        /// 网页标签
        /// </summary>
        [EnumDescription("网页标签",RegexStrings.HtmlLabelRegex)]
        HtmlLabel =1,
        /// <summary>
        /// 正整数
        /// </summary>
        [EnumDescription("正整数",RegexStrings.PositiveIntegerRegex)]
        PositiveInteger,
        /// <summary>
        /// 负整数
        /// </summary>
        [EnumDescription("负整数", RegexStrings.NegativeIntegerRegex)]
        NegativeInteger,
        /// <summary>
        /// 整数
        /// </summary>
        [EnumDescription("整数", RegexStrings.IntegerRegex)]
        Integer,
        /// <summary>
        /// 正浮点数
        /// </summary>
        [EnumDescription("正浮点数", RegexStrings.PositiveFloatRegex)]
        PositiveFloat,
        /// <summary>
        /// 负浮点数
        /// </summary>
        [EnumDescription("负浮点数", RegexStrings.NegativeFloatRegex)]
        NegativeFloat,
        /// <summary>
        /// 浮点数
        /// </summary>
        [EnumDescription("浮点数", RegexStrings.FloatRegex)]
        Float,
        /// <summary>
        /// 26个英文字(不区分大小写)
        /// </summary>
        [EnumDescription("26个英文字(不区分大小写)", RegexStrings.LetterRegex)]
        Letter,
        /// <summary>
        /// 26个大写英文字母
        /// </summary>
        [EnumDescription("26个大写英文字母", RegexStrings.CapitalLetterRegex)]
        CapitalLetter,
        /// <summary>
        /// 26个小写英文字母
        /// </summary>
        [EnumDescription("26个小写英文字母", RegexStrings.LowercaseRegex)]
        Lowercase,
        /// <summary>
        /// 数字和26个英文字母混合
        /// </summary>
        [EnumDescription("数字和26个英文字母混合", RegexStrings.NumberAndLetterRegex)]
        NumberAndLetter,
        /// <summary>
        /// 数字和26个英文字母和下划线混合
        /// </summary>
        [EnumDescription("数字和26个英文字母和下划线混合",RegexStrings.NumberLetterUnderlineRegex)]
        NumberLetterUnderline,
        /// <summary>
        /// Email地址正则表达式
        /// </summary>
        [EnumDescription("Email地址正则表达式", RegexStrings.EmailRegex)]
        Email,
        /// <summary>
        /// Url正则表达式
        /// </summary>
        [EnumDescription("Url正则表达式", RegexStrings.UrlRegex)]
        Url,
        /// <summary>
        /// 中国电话: 例如：021-8888888 或者0515-88888888 或者 021-88888888-8888
        /// </summary>
        [EnumDescription("中国电话", RegexStrings.ChinaPhoneRegex)]
        ChinaPhone,
        /// <summary>
        ///中国邮政编码 
        /// </summary>
        [EnumDescription("中国邮政编码", RegexStrings.ChinaPostCodeRegex)]
        ChinaPost,
        /// <summary>
        /// 中国手机
        /// </summary>
        [EnumDescription("中国手机", RegexStrings.ChinaMobileRegex)]
        ChinaMobile,
        /// <summary>
        /// 中国电话包括手机
        /// </summary>
        [EnumDescription("中国电话包括手机", RegexStrings.ChinaPhoneAndMobileRegex)]
        ChinaPhoneAndMobile,
        /// <summary>
        /// QQ号
        /// </summary>
        [EnumDescription("QQ号", RegexStrings.QqRegex)]
        Qq,
        /// <summary>
        /// IP地址
        /// </summary>
        [EnumDescription("IP地址", RegexStrings.IpRegex)]
        Ip,
        /// <summary>
        /// 中国身份证号
        /// </summary>
        [EnumDescription("中国身份证号", RegexStrings.IdentityNo)]
        IdentityNo
    }
}
