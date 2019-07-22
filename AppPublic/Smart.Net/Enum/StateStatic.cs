namespace Smart.Net45.Enum
{
    /// <summary>
    /// states 类
    /// </summary>
    public static class StateStatic
    {
        /// <summary>
        /// 设置为可用
        /// </summary>
        /// <param name="statesKinds"></param>
        /// <returns></returns>
        public static StatesKinds SetEnable(this StatesKinds statesKinds)
        {

            if (statesKinds.HasFlag(StatesKinds.DissEnable))
                statesKinds = statesKinds ^ StatesKinds.DissEnable;
            if (!statesKinds.HasFlag(StatesKinds.Enable))
                statesKinds = statesKinds | StatesKinds.Enable;
            return statesKinds;
        }
        /// <summary>
        /// 设置不可用
        /// </summary>
        /// <param name="statesKinds"></param>
        /// <returns></returns>
        public static StatesKinds SetDissEnable(this StatesKinds statesKinds)
        {
            if (statesKinds.HasFlag(StatesKinds.Enable))
                statesKinds = statesKinds ^ StatesKinds.Enable;
            if (!statesKinds.HasFlag(StatesKinds.DissEnable))
                statesKinds = statesKinds | StatesKinds.DissEnable;
            return statesKinds;
        }
        /// <summary>
        /// 设置锁定
        /// </summary>
        /// <param name="statesKinds"></param>
        /// <returns></returns>
        public static StatesKinds SetLock(this StatesKinds statesKinds)
        {
            if (statesKinds.HasFlag(StatesKinds.UnLock))
                statesKinds = statesKinds ^ StatesKinds.UnLock;
            if (!statesKinds.HasFlag(StatesKinds.Lock))
                statesKinds = statesKinds | StatesKinds.Lock;
            return statesKinds;
        }
        /// <summary>
        /// 设置解锁
        /// </summary>
        /// <param name="statesKinds"></param>
        /// <returns></returns>
        public static StatesKinds SetUnLock(this StatesKinds statesKinds)
        {
            if (statesKinds.HasFlag(StatesKinds.Lock))
                statesKinds = statesKinds ^ StatesKinds.Lock;
            if (!statesKinds.HasFlag(StatesKinds.UnLock))
                statesKinds = statesKinds | StatesKinds.UnLock;
            return statesKinds;
        }
    }
}
