namespace Smart.Standard.Extends
{
    /// <summary>
    /// Int扩展类
    /// </summary>
    public static class IntExtends
    {
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="states"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag(this int states,int flag)
        {
            return (states & flag) == flag;
        }
        /// <summary>
        /// 判断是否存在该状态
        /// </summary>
        /// <param name="states"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static bool HasFlag(this int states, System.Enum flag)
        {
            return (states & flag.CastTo<int>()) == flag.CastTo<int>();
        }
    }
}
