/*
 * SQL Server 2008除了DateTime和SmallDateTime之外，又新增了四种时间类型，分别为：date，time，datetime2和datetimeoffset。
 * 各时间类型范围、精度一览表：
 * 数据类型 时间范围 精度
 * datetime 1753-01-01到9999-12-31 00:00:00 到 23:59:59.997 3.33毫秒
 * smalldatetime 1900-01-01 到 2079-06-06 00:00:00 到 23:59:59 分钟
 * date 0001-01-01 到 9999-12-31 天
 * time 00:00:00.0000000 到 23:59:59.9999999 100 纳秒
 * datetime2 0001-01-01 到 9999-12-31 00:00:00 到 23:59:59.9999999 100 纳秒
 * datetimeoffset 0001-01-01 到 9999-12-31 00:00:00 到 23:59:59.9999999 -14:00 到 +14:00 100 纳秒
 * 各时间类型表达式一览表： 数据类型 输出 time 12:35:29. 1234567 date 2007-05-08 smalldatetime 2007-05-08 12:35:00 datetime 2007-05-08 12:35:29.123 datetime2 2007-05-08 12:35:29. 1234567 datetimeoffset 2007-05-08 12:35:29.1234567 +12:15
 * */

using System;
using Smart.Net45.Helper;

namespace Smart.Net45.Extends
{
    /// <summary>
    /// 日期类型的扩展方法
    /// </summary>
    public static class DateTimeExtends
    {
        /// <summary>
        /// T-SQL最小时间，按照smalldatetime作为依据确定
        /// </summary>
        private static readonly DateTime MinSqlServerDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
        /// <summary>
        /// T-SQL最大时间，按照smalldatetime作为依据确定
        /// </summary>
        private static readonly DateTime MaxSqlServerDateTime = new DateTime(2079, 6, 6, 23, 59, 59);
        /// <summary>
        /// 转为日期字符串
        /// </summary>
        /// <param name="dateTime">可空日期</param>
        /// <returns>yyyy-MM-dd表示的日期，或空字符串</returns>
        public static string ToDateString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateString() : string.Empty;
        }
        /// <summary>
        /// 转为日期字符串
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>yyyy-MM-dd表示的日期，或空字符串</returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? string.Empty : dateTime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 转为日期时间字符串
        /// </summary>
        /// <param name="dateTime">可空日期</param>
        /// <returns>yyyy-MM-dd HH:mm表示的日期，或空字符串</returns>
        public static string ToDateTimeString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToDateTimeString() : string.Empty;
        }
        /// <summary>
        /// 转为日期时间字符串，精确到分
        /// <para>格式：yyyy-MM-dd HH:mm</para>
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>yyyy-MM-dd HH:mm表示的日期，或空字符串</returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? string.Empty : dateTime.ToString("yyyy-MM-dd HH:mm");
        }
        /// <summary>
        /// 转为日期时间字符串,精确到秒 
        /// <para>格式：yyyy-MM-dd HH:mm:ss</para>
        /// </summary>
        public static string ToDateTimeStringSecond(this DateTime dateTime)
        {
            return dateTime == DateTime.MinValue ? string.Empty : dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 判断日期时间是否是合法的数据库日期时间值
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>有效true，无效false</returns>
        public static bool IsValidSqlServerDateTime(this DateTime dateTime)
        {
            return dateTime >= MinSqlServerDateTime && dateTime <= MaxSqlServerDateTime;
        }
        /// <summary>
        /// 获取有效的SQL时间或当前日期
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>有效日期本身，无效DateTime.Now</returns>
        public static DateTime GetValidSqlServerDateTimeOrNow(this DateTime dateTime)
        {
            return dateTime.IsValidSqlServerDateTime() ? dateTime : DateTime.Now;
        }       
        /// <summary>
        /// 转换为Unix时间戳（毫秒）
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>毫秒</returns>
        public static long ToUnixTimestamp(this DateTime dateTime)
        {
            return DateTimeHelper.GetUnixTimestamp(dateTime);
        }
        /// <summary>
        /// 转换为Unix时间戳（秒）
        /// </summary>
        /// <param name="dateTime">日期</param>
        /// <returns>秒</returns>
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            return DateTimeHelper.GetNowUnixTimeSeconds(dateTime);
        }
        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string GetLunarDateTime(this DateTime dateTime)
        {
            return LunarDateTimeHelper.GetChineseDateTime(dateTime);
        }
        /// <summary>
        /// 获取本周周一
        /// </summary>
        /// <returns></returns>
        public static DateTime ThisMonday(this DateTime dateTime)
        {
            return dateTime.AddDays(1 - dateTime.DayOfWeek.CastTo<int>());
        }
        /// <summary>
        /// 获取下周周一
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime NextMonday(this DateTime dateTime)
        {
            return dateTime.ThisMonday().AddDays(7);
        }
        /// <summary>
        /// 获取本月1号
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ThisMonthStart(this DateTime dateTime)
        {
            return dateTime.AddDays(1 - dateTime.Day);
        }
        /// <summary>
        /// 获取下月一号
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime NextMonthStart(this DateTime dateTime)
        {

            return dateTime.ThisMonday().AddMonths(1);
        }
    }
}
