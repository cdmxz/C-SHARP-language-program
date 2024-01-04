using 腾讯会议摸鱼助手.TengXunMeetingHelper.Common;
namespace 腾讯会议摸鱼助手.TengXunMeetingHelper.Keyboard
{
    internal class KeyboardEvent
    {
        /// <summary>
        /// 发送ctrl+v
        /// </summary>
        public static void Paste()
        {
            // 模拟按下按键
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            WinApi.keybd_event((byte)Keys.V, (byte)WinApi.MapVirtualKey((uint)Keys.V, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(50);
            WinApi.keybd_event((byte)Keys.V, (byte)WinApi.MapVirtualKey((uint)Keys.V, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// 全选
        /// </summary>
        public static void SelectAll()
        {
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            WinApi.keybd_event((byte)Keys.A, (byte)WinApi.MapVirtualKey((uint)Keys.A, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(50);
            WinApi.keybd_event((byte)Keys.A, (byte)WinApi.MapVirtualKey((uint)Keys.A, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
        }

        public static void Cut()
        {
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            WinApi.keybd_event((byte)Keys.X, (byte)WinApi.MapVirtualKey((uint)Keys.A, WinApi.MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(50);
            WinApi.keybd_event((byte)Keys.X, (byte)WinApi.MapVirtualKey((uint)Keys.A, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
            WinApi.keybd_event((byte)Keys.ControlKey, (byte)WinApi.MapVirtualKey((uint)Keys.ControlKey, WinApi.MAPVK_VK_TO_VSC), WinApi.KEYEVENTF_KEYUP, 0);
        }
    }
}
