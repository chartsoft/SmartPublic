﻿using System;
using System.Globalization;

namespace Smart.Net45.Helper
{
    /// <summary>
    /// 农历日期辅助类
    /// </summary>
    public class LunarDateTimeHelper
    {       
        ///<summary>
        /// 实例化一个 ChineseLunisolarCalendar
        ///</summary>
        private static readonly ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
        ///<summary>
        /// 十天干
        ///</summary>
        private static readonly string[] Tg = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        ///<summary>
        /// 十二地支
        ///</summary>
        private static readonly string[] Dz = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        ///<summary>
        /// 十二生肖
        ///</summary>
        private static readonly string[] Sx = { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
        ///<summary>
        /// 返回农历天干地支年
        ///</summary>
        ///<param name="year">农历年</param>
        public static string GetLunisolarYear(int year)
        {
            if (year <= 3) throw new ArgumentOutOfRangeException("无效的年份!");
            var tgIndex = (year - 4) % 10;
            var dzIndex = (year - 4) % 12;
            return string.Concat(Tg[tgIndex], Dz[dzIndex], "[", Sx[dzIndex], "]");
        }
        ///<summary>
        /// 农历月
        ///</summary>
        private static readonly string[] Months = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二(腊)" };
        ///<summary>
        /// 农历日
        ///</summary>
        private static readonly string[] Days1 = { "初", "十", "廿", "三" };
        ///<summary>
        /// 农历日
        ///</summary>
        private static readonly string[] Days = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
        ///<summary>
        /// 返回农历月
        ///</summary>
        ///<param name="month">月份</param>
        public static string GetLunisolarMonth(int month)
        {
            if (month < 13 && month > 0)
            {
                return Months[month - 1];
            }

            throw new ArgumentOutOfRangeException("无效的月份!");
        }
        ///<summary>
        /// 返回农历日
        ///</summary>
        ///<param name="day">天</param>
        public static string GetLunisolarDay(int day)
        {
            if (day <= 0 || day >= 32) throw new ArgumentOutOfRangeException("无效的日!");
            if (day != 20 && day != 30)
            {
                return string.Concat(Days1[(day - 1) / 10], Days[(day - 1) % 10]);
            }
            return string.Concat(Days[(day - 1) / 10], Days1[1]);
        }
        ///<summary>
        /// 根据公历获取农历日期
        ///</summary>
        ///<param name="datetime">公历日期</param>
        public static string GetChineseDateTime(DateTime datetime)
        {
            var year = ChineseCalendar.GetYear(datetime);
            var month = ChineseCalendar.GetMonth(datetime);
            var day = ChineseCalendar.GetDayOfMonth(datetime);
            //获取闰月， 0 则表示没有闰月
            var leapMonth = ChineseCalendar.GetLeapMonth(year);

            var isleap = false;

            if (leapMonth > 0)
            {
                if (leapMonth == month)
                {
                    //闰月
                    isleap = true;
                    month--;
                }
                else if (month > leapMonth)
                {
                    month--;
                }
            }

            return string.Concat(GetLunisolarYear(year), "年", isleap ? "闰" : string.Empty, GetLunisolarMonth(month), "月", GetLunisolarDay(day));
        }
    }
}
