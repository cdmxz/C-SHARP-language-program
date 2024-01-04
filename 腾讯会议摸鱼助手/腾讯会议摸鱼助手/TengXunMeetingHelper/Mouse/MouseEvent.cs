using 腾讯会议摸鱼助手.TengXunMeetingHelper.Common;

namespace 腾讯会议摸鱼助手.TengXunMeetingHelper.Mouse
{
    internal class MouseEvent
    {
        /// <summary>
        /// 设置鼠标坐标
        /// </summary>
        /// <param name="p"></param>
        public static void SetCursorPos(Point p)
        {
            WinApi.SetCursorPos(p.X, p.Y);
        }

        /// <summary>
        /// 获取鼠标坐标
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPos()
        {
            WinApi.GetCursorPos(out Point p);
            return p;
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        public static void MouseLeftClick()
        {
            // 鼠标右键按下
            WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep(50);
            // 鼠标右键抬起
            WinApi.mouse_event(WinApi.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        /// <param name="p"></param>
        public static void MouseLeftClickByPoint(Point p)
        {
            SetCursorPos(p);
            MouseLeftClick();
        }

    }
}
