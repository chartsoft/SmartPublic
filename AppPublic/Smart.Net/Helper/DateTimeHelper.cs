using System;

namespace Smart.Net45.Helper
{

    /// <summary>
    /// 日期时间辅助类
    /// </summary>
    public class DateTimeHelper
    {


        /// <summary>
        /// 获得Unix时间戳（毫秒）
        /// </summary>
        /// <returns>毫秒</returns>
        public static long GetUnixTimestamp(DateTime time)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - startTime).TotalMilliseconds;
        }
        /// <summary>
        /// 获得当前Unix时间戳（毫秒）
        /// </summary>
        /// <returns>毫秒</returns>
        public static long GetNowUnixTimestamp()
        {
            return GetUnixTimestamp(DateTime.Now);
        }
        /// <summary>
        /// 获得当前Unix时间戳（秒）
        /// </summary>
        /// <returns>秒</returns>
        public static long GetNowUnixTimeSeconds()
        {
            return GetNowUnixTimeSeconds(DateTime.Now);
        }
        /// <summary>
        /// 获得当前Unix时间戳（秒）
        /// </summary>
        /// <returns>秒</returns>
        public static long GetNowUnixTimeSeconds(DateTime time)
        {
            var startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            //var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time.AddHours(-8) - startTime).TotalSeconds;
        }

        /// <summary>
        /// 时间戳转换为日期（毫秒级)
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixStampToDateTime(long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddMilliseconds(timeStamp).AddHours(8);
        }

        /// <summary>
        /// 时间戳转换为日期（秒级）
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixSecondsToDateTime(long timeStamp)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(timeStamp).AddHours(8);
        }

    }
}
