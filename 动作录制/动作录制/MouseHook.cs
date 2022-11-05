using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows;

namespace 动作录制
{
    class MouseHook : IDisposable
    {
        public delegate void MouseKeyEventHandle(object sender, Keys key, DateTime time);
        public event MouseKeyEventHandle MouseDownEvent; // 按下按键事件
        public event MouseKeyEventHandle MouseUpEvent;   // 松开按键事件
        public delegate void MouseWheelEventHandle(object sender, int wheel, DateTime time);
        public event MouseWheelEventHandle MouseWheelEvent; // 移动滚轮事件
        public delegate void MouseMoveEventHandle(object sender, Point point, DateTime time);
        public event MouseMoveEventHandle MouseMoveEvent;   // 移动鼠标事件
        public delegate void MouseKeyDoubleClickEventHandle(object sender, Keys key, DateTime time);

        private Api.HOOKPROC hookProc;// 回调函数
        private bool installed;       // 是否安装钩子
        private IntPtr moduleHandle;  // 当前模块句柄
        private IntPtr hookHandle;    // 钩子句柄

        public MouseHook()
        {
            hookProc = new Api.HOOKPROC(LowLevelMouseProc);// 回调函数
                                                           // 当前程序的进程句柄
            moduleHandle = Api.GetModuleHandleW(Process.GetCurrentProcess().MainModule.ModuleName);
        }

        // 安装钩子
        public bool InstallHotKey()
        {
            if (installed)
                UnHotKey();
            hookHandle = Api.SetWindowsHookExW(Api.WH_MOUSE_LL, hookProc, moduleHandle, 0);
            installed = !(hookHandle == IntPtr.Zero);
            return installed;
        }

        // 卸载钩子
        public void UnHotKey()
        {
            if (installed)
                Api.UnhookWindowsHookEx(hookHandle);
            installed = false;
        }

        private void OnMouseDown(Keys key) => MouseDownEvent?.Invoke(this, key, DateTime.Now);
        private void OnMouseUp(Keys key) => MouseUpEvent?.Invoke(this, key, DateTime.Now);
        private void OnMouseMove(Point point) => MouseMoveEvent?.Invoke(this, point, DateTime.Now);
        private void OnMouseWheel(int wheel) => MouseWheelEvent?.Invoke(this, wheel, DateTime.Now);

        // 鼠标钩子回调函数
        private int LowLevelMouseProc(int nCode, int wParam, IntPtr lParam)
        {
            // 如果该消息被丢弃（nCode<0）则不会触发事件
            if (nCode >= 0)
            {
                Api.TagMOUSEHOOKSTRUCT hookStruct = (Api.TagMOUSEHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Api.TagMOUSEHOOKSTRUCT));
                Point pt = new Point(hookStruct.pt.x, hookStruct.pt.y);
                OnMouseMove(pt);// 触发鼠标移动事件
                switch (wParam)
                {
                    case Api.WM_LBUTTONDOWN:
                        OnMouseDown(Keys.LButton);
                        break;
                    case Api.WM_MBUTTONDOWN:
                        OnMouseDown(Keys.MButton);
                        break;
                    case Api.WM_RBUTTONDOWN:
                        OnMouseDown(Keys.RButton);
                        break;
                    case Api.WM_LBUTTONUP:
                        OnMouseUp(Keys.LButton);
                        break;
                    case Api.WM_MBUTTONUP:
                        OnMouseUp(Keys.MButton);
                        break;
                    case Api.WM_RBUTTONUP:
                        OnMouseUp(Keys.RButton);
                        break;
                    case Api.WM_MOUSEWHEEL:
                        OnMouseWheel((short)((hookStruct.mouseData >> 16) & 0xffff));
                        break;
                }
            }
            return Api.CallNextHookEx(hookHandle, nCode, wParam, lParam);
        }

        public void Dispose() => UnHotKey();
        // 析构函数
        ~MouseHook() => Dispose();
    }
}
