using System;

namespace Smart.Win.Controls
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    public delegate void SplashNoticeMsgEventHandler(string msg);

    /// <summary>
    /// 
    /// </summary>
    public class SplashNotice
    {
        /// <summary>
        /// 
        /// </summary>
        public static event SplashNoticeMsgEventHandler SplashNoticeMsg;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public static void Notice(string msg)
        {
            if (SplashNoticeMsg != null)
            {
                SplashNoticeMsg(msg);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static event Action FinishWork;
        /// <summary>
        /// 
        /// </summary>
        public static void OnFinishWork()
        {
            if (FinishWork != null)
            {
                FinishWork();
            }
        }

    }
}
