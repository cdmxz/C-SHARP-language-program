using System.Runtime.InteropServices;
using System.Text;
using 腾讯会议摸鱼助手.TengXunMeetingHelper.Common;
using 腾讯会议摸鱼助手.TengXunMeetingHelper.Keyboard;
using 腾讯会议摸鱼助手.TengXunMeetingHelper.Mouse;

namespace 腾讯会议摸鱼助手.TengXunMeetingHelper
{
    internal class MeetingHelper
    {
        /// <summary>
        /// 改名
        /// </summary>
        /// <param name="newName">新名字</param>
        /// <returns>旧名字</returns>
        public static string ChangeName(string newName)
        {
            if(TengxunMeetingLiveWindowMinimize())
                throw new Exception("请不要最小化会议窗口");
            Point cur = MouseEvent.GetCursorPos();
            // 点击成员按钮
            MouseLeftClickByRelativePoint(RelativePositionData.MemberButton);
            Thread.Sleep(500);
            // 点击改名按钮
            MouseLeftClickByRelativePoint(RelativePositionData.ChangeNameButton);
            Thread.Sleep(500);
            // 剪切旧名字
            KeyboardEvent.SelectAll();
            KeyboardEvent.Cut();
            Thread.Sleep(500);
            string oldName = Clipboard.GetText();
            // 粘贴新名字
            Clipboard.SetText(newName);
            KeyboardEvent.Paste();
            // 清空剪切板
            Clipboard.Clear();
            Thread.Sleep(500);
            // 点击确定按钮
            MouseLeftClickByRelativePoint(RelativePositionData.OkButton);
            Thread.Sleep(500);
            // 再点击成员按钮关闭成员窗口，方便下次点击
            MouseLeftClickByRelativePoint(RelativePositionData.MemberButton);
            MouseEvent.SetCursorPos(cur);
            return oldName;
        }


        /// <summary>
        /// 发送聊天文本
        /// 
        /// 发送前请手动点击“聊天”按钮打开聊天板
        /// 
        /// </summary>
        /// <param name="text"></param>
        public static void SendText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            if (TengxunMeetingLiveWindowMinimize())
                throw new Exception("请不要最小化会议窗口");
            Point cur = MouseEvent.GetCursorPos();
            MouseLeftClickByRelativePoint(RelativePositionData.InputBox);
            Thread.Sleep(500);
            Clipboard.SetText(text);
            KeyboardEvent.Paste();
            Clipboard.Clear();
            MouseEvent.SetCursorPos(cur);
            MouseLeftClickByRelativePoint(RelativePositionData.SendButton);
        }

        /// <summary>
        /// 离开腾讯会议
        /// </summary>
        public static void LeaveMeeting()
        {
            if (TengxunMeetingLiveWindowMinimize())
                throw new Exception("请不要最小化会议窗口");
            // 点击离开会议按钮
            Point cur = MouseEvent.GetCursorPos();
            MouseLeftClickByRelativePoint(RelativePositionData.LeaveButton);
            MouseEvent.SetCursorPos(cur);
        }

        /// <summary>
        /// 鼠标左键点击 使用相对坐标
        /// </summary>
        /// <param name="p"></param>
        public static void MouseLeftClickByRelativePoint(Point p)
        {
             if(TengxunMeetingLiveWindowMinimize())
                throw new Exception("请不要最小化会议窗口");
            ActivateLiveWindow();
            Thread.Sleep(500);
            MouseEvent.MouseLeftClickByPoint(TengxunMeetingToScreenPoint(p));
        }

        /// <summary>
        /// 屏幕坐标 转 腾讯会议的坐标
        /// </summary>
        /// <param name="screenPoint"></param>
        /// <returns></returns>
        public static Point ScreenToTengxunMeetingPoint(Point screenPoint)
        {
            // 获取窗口句柄
            IntPtr window = FindTengxunMeetingLiveWindowHandle(out _);
            Point p = screenPoint;
            WinApi.ScreenToClient(window, ref p);
            return p;
        }

        /// <summary>
        /// 腾讯会议坐标 转 屏幕坐标
        /// </summary>
        /// <param name="tengxunPoint"></param>
        /// <returns></returns>
        public static Point TengxunMeetingToScreenPoint(Point tengxunPoint)
        {
            IntPtr window = FindTengxunMeetingLiveWindowHandle(out _);
            WinApi.ClientToScreen(window, ref tengxunPoint);
            return tengxunPoint;
        }

        /// <summary>
        /// 获取腾讯会议的直播窗口坐标
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IntPtr FindTengxunMeetingLiveWindowHandle(out bool minimize)
        {
            minimize = false;
            IntPtr handle = FindWindowByClassAndStyle("TXGuiFoundation", 0x96070000);
            if (handle == IntPtr.Zero)
            {
                handle = FindWindowByClassAndStyle("TXGuiFoundation", 0x960D0000);
                minimize = true;
            }
            //    throw new Exception("无法获取腾讯会议的直播窗口坐标");
            return handle;
        }

        /// <summary>
        /// 检查腾讯会议的直播窗口坐标是否存在
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool CheckTengxunMeetingLiveWindowHandle()
        {
            IntPtr handle = FindTengxunMeetingLiveWindowHandle(out _);
            return (handle != IntPtr.Zero);
        }

        /// <summary>
        /// 检查腾讯会议的直播窗口是否最小化
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool TengxunMeetingLiveWindowMinimize()
        {
            FindTengxunMeetingLiveWindowHandle(out bool minimize);
            return minimize;
        }

        /// <summary>
        /// 激活会议窗口
        /// </summary>
        public static void ActivateLiveWindow()
        {
            IntPtr window = FindTengxunMeetingLiveWindowHandle(out _);
            WinApi.SetForegroundWindow(window);
        }

        /// <summary>
        /// 根据 窗口样式和窗口标题 查找 窗口句柄
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        public static IntPtr FindWindowByTitleAndStyle(string windowTitle, uint windowStyle)
        {
            IntPtr result = IntPtr.Zero;
            // 枚举所有父窗口
            WinApi.EnumWindows((hwnd, _) =>
            {
                // 只查找可见窗口
                if (WinApi.IsWindowVisible(hwnd))
                {
                    WinApi.WINDOWINFO winInfo = new();
                    winInfo.cbSize = (uint)Marshal.SizeOf(typeof(WinApi.WINDOWINFO));
                    WinApi.GetWindowInfo(hwnd, ref winInfo);
                    StringBuilder sb = new(260);
                    WinApi.GetWindowTextW(hwnd, sb, 260);
                    if (sb.ToString().Equals(windowTitle) && winInfo.dwStyle == windowStyle)
                    {
                        result = hwnd;
                        return false;// 返回false停止枚举窗口
                    }
                }
                return true;
            }, IntPtr.Zero);

            return result;
        }

        /// <summary>
        /// 根据 窗口样式和窗口类名 查找 窗口句柄
        /// </summary>
        /// <param name="windowClass"></param>
        /// <param name="windowStyle"></param>
        /// <returns></returns>
        public static IntPtr FindWindowByClassAndStyle(string windowClass, uint windowStyle)
        {
            IntPtr result = IntPtr.Zero;
            // 枚举所有父窗口
            WinApi.EnumWindows((hwnd, _) =>
            {
                // 只查找可见窗口
                if (WinApi.IsWindowVisible(hwnd))
                {
                    WinApi.WINDOWINFO winInfo = new();
                    winInfo.cbSize = (uint)Marshal.SizeOf(typeof(WinApi.WINDOWINFO));
                    WinApi.GetWindowInfo(hwnd, ref winInfo);
                    StringBuilder sb = new(260);
                    WinApi.GetClassNameW(hwnd, sb, 260);
                    if (sb.ToString().Equals(windowClass) && winInfo.dwStyle == windowStyle)
                    {
                        result = hwnd;
                        return false;// 返回false停止枚举窗口
                    }
                }
                return true;
            }, IntPtr.Zero);

            return result;
        }

        /// <summary>
        /// 通过窗体句柄获取窗体大小（不包括阴影部分）
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static Rectangle GetWindowSizeByHandle(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return new Rectangle();
            _ = WinApi.DwmGetWindowAttribute(hWnd, WinApi.DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out WinApi.TagRECT rect, Marshal.SizeOf(typeof(WinApi.TagRECT)));
            return rect.ToRectangle();
        }
    }
}
