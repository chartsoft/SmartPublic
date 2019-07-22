using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Smart.Win.Helpers
{
    /// <summary>
    /// 平台互操作辅助类
    /// </summary>
    public partial class PInvokeHelper
    {
        /// <summary>
        /// 获取键状态
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(int keyCode);

        /// <summary>
        /// 改变键状态
        /// </summary>
        /// <param name="bVk"></param>
        /// <param name="bScan"></param>
        /// <param name="dwFlags"></param>
        /// <param name="dwExtraInfo"></param>
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        //按键状态
        private const int KEYEVENTF_EXTENDEDKEY = 0x1;
        private const int KEYEVENTF_KEYUP = 0x2;
        //键值
        private const int VK_NUMLOCK = 0x90;
        private const int VK_CAPITAL = 0x14;
        private const int VK_SCROLL = 0x91;

        /// <summary>
        /// 设置NUM Lock
        /// </summary>
        public static void ChangeNumLock()
        {
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(VK_NUMLOCK, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            Application.DoEvents();
        }

        /// <summary>
        /// 设置Scroll Lock
        /// </summary>
        public static void ChangeScrollLock()
        {
            keybd_event(VK_CAPITAL, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(VK_CAPITAL, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            Application.DoEvents();
        }

        /// <summary>
        /// 设置Caps Lock
        /// </summary>
        public static void ChangeCapsLock()
        {
            keybd_event(VK_SCROLL, 0x45, KEYEVENTF_EXTENDEDKEY, (UIntPtr)0);
            keybd_event(VK_SCROLL, 0x45, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, (UIntPtr)0);
            Application.DoEvents();
        }

        /// <summary>
        /// 是否CapsLock开启
        /// </summary>
        public static bool IsCapsLock
        {
            get
            {
                var capsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
                return capsLock;
            }
        }

        /// <summary>
        /// 是否NumLock开启
        /// </summary>
        public static bool IsNumLock
        {
            get
            {
                var numLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
                return numLock;
            }
        }

        /// <summary>
        /// 是否ScrollLock开启
        /// </summary>
        public static bool IsScrollLock
        {
            get
            {
                var scrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
                return scrollLock;
            }
        }

    }
}