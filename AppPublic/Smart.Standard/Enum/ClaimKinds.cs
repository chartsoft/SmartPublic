using Smart.Standard.Attribute;

namespace Smart.Standard.Enum
{
    /// <summary>
    /// Token声明创建
    /// </summary>
    public enum ClaimKinds
    {

        /// <summary>
        /// jwt签发者
        /// </summary>
        [EnumDescription("jwt签发者")]
        Iss=1,
        /// <summary>
        /// jwt所面向的用户
        /// </summary>
        [EnumDescription("jwt所面向的用户")]
        Sub=2,
        /// <summary>
        /// 接收jwt的一方
        /// </summary>
        [EnumDescription("接收jwt的一方")]
        Aud=3,
        /// <summary>
        /// jwt的过期时间
        /// </summary>
        [EnumDescription("jwt的过期时间，这个过期时间必须要大于签发时间")]
        Exp=4,
        /// <summary>
        /// 定义在什么时间之前，该jwt都是不可用的
        /// </summary>
        [EnumDescription("定义在什么时间之前，该jwt都是不可用的")]
        Nbf=5,
        /// <summary>
        /// jwt的签发时间
        /// </summary>
        [EnumDescription("jwt的签发时间")]
        Iat =6,
        /// <summary>
        /// jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击
        /// </summary>
        [EnumDescription("jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击")]
        Jti=7,
        /// <summary>
        ///自定义声明
        /// </summary>
        [EnumDescription("自定义声明")]
        CusClaim=8,
    }
}
