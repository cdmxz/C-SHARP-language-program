using System.Diagnostics;
using System.Runtime.InteropServices;

namespace 腾讯会议摸鱼助手.Hook
{
    public delegate void MouseKeyEventHandle(Keys key, DateTime time);
    public delegate void MouseWheelEventHandle(int wheel, DateTime time);
    public delegate void MouseMoveEventHandle(Point point, DateTime time);
    public delegate void MouseKeyDoubleClickEventHandle(Keys key, DateTime time);

    class MouseHook : IDisposable
    {
        /// <summary>
        /// 按下按键事件
        /// </summary>
        public event MouseKeyEventHandle? MouseDownEvent;
        /// <summary>
        /// 松开按键事件
        /// </summary>
        public event MouseKeyEventHandle? MouseUpEvent;
        /// <summary>
        /// 移动滚轮事件
        /// </summary>
        public event MouseWheelEventHandle? MouseWheelEvent;
        /// <summary>
        /// 移动鼠标事件
        /// </summary>
        public event MouseMoveEventHandle? MouseMoveEvent;
        /// <summary>
        /// 是否安装了钩子
        /// </summary>
        public bool Installed { get => installed; }

        public bool installed;                     // 是否安装钩子
        public readonly WinApi.HOOKPROC hookProc;  // 回调函数
        public readonly IntPtr moduleHandle;       // 当前模块句柄
        public IntPtr hookHandle;                  // 钩子句柄

        public MouseHook()
        {
            installed = false;
            hookProc = new WinApi.HOOKPROC(LowLevelMouseProc);// 回调函数
            string? module = Process.GetCurrentProcess()?.MainModule?.ModuleName;
            if (module == null)
                throw new Exception("无法获取当前模块名称");
            // 当前程序的进程句柄
            moduleHandle = WinApi.GetModuleHandleW(module);
        }

        /// <summary>
        /// 安装钩子
        /// </summary>
        /// <returns></returns>
        public bool InstallHotKey()
        {
            if (installed)
                UnHotKey();
            hookHandle = WinApi.SetWindowsHookExW(WinApi.WH_MOUSE_LL, hookProc, moduleHandle, 0);
            if (hookHandle == IntPtr.Zero)
                throw new Exception("无法安装鼠标钩子！");
            installed = true;
            return installed;
        }

        /// <summary>
        /// 卸载钩子
        /// </summary>
        public void UnHotKey()
        {
            WinApi.UnhookWindowsHookEx(hookHandle);
            installed = false;
        }

        public void OnMouseDown(Keys key) => MouseDownEvent?.Invoke(key, DateTime.Now);
        public void OnMouseUp(Keys key) => MouseUpEvent?.Invoke(key, DateTime.Now);
        public void OnMouseMove(Point point) => MouseMoveEvent?.Invoke(point, DateTime.Now);
        public void OnMouseWheel(int wheel) => MouseWheelEvent?.Invoke(wheel, DateTime.Now);

        /// <summary>
        /// 鼠标钩子回调函数
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public int LowLevelMouseProc(int nCode, int wParam, IntPtr lParam)
        {
            // 如果该消息被丢弃（nCode<0）则不会触发事件
            if (nCode >= 0)
            {
                WinApi.TagMOUSEHOOKSTRUCT hookStruct = (WinApi.TagMOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(WinApi.TagMOUSEHOOKSTRUCT));
                OnMouseMove(hookStruct.pt);// 触发鼠标移动事件
                switch (wParam)
                {
                    case WinApi.WM_LBUTTONDOWN:
                        OnMouseDown(Keys.LButton);
                        break;
                    case WinApi.WM_MBUTTONDOWN:
                        OnMouseDown(Keys.MButton);
                        break;
                    case WinApi.WM_RBUTTONDOWN:
                        OnMouseDown(Keys.RButton);
                        break;
                    case WinApi.WM_LBUTTONUP:
                        OnMouseUp(Keys.LButton);
                        break;
                    case WinApi.WM_MBUTTONUP:
                        OnMouseUp(Keys.MButton);
                        break;
                    case WinApi.WM_RBUTTONUP:
                        OnMouseUp(Keys.RButton);
                        break;
                    case WinApi.WM_MOUSEWHEEL:
                        OnMouseWheel((short)((hookStruct.mouseData >> 16) & 0xffff));
                        break;
                }
            }
            return WinApi.CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }

        public void Dispose()
        {
            UnHotKey();
            GC.SuppressFinalize(this);
        }

        // 析构函数
        ~MouseHook() => Dispose();
    }
}
