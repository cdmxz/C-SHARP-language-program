using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace 动作录制
{
    class KeyboardHook : IDisposable
    {
        public delegate void KeyEventHandle(object sender, KeyEventArgs key, DateTime time);
        public event KeyEventHandle KeyDownEvent; // 按下按键事件
        public event KeyEventHandle KeyUpEvent; // 松开按键事件
        private Api.HOOKPROC hookProc; // 回调函数
        private bool installed;  // 是否安装钩子
        private IntPtr moduleHandle; // 模块句柄
        private IntPtr hookHandle; // 钩子句柄

        public KeyboardHook()
        {
            hookProc = new Api.HOOKPROC(HookProc);// 回调函数
            // 获取当前程序的进程句柄
            moduleHandle = Api.GetModuleHandleW(Process.GetCurrentProcess().MainModule.ModuleName);
        }

        // 安装钩子
        public bool InstallHotKey()
        {
            if (installed)
                UnHotKey();
            hookHandle = Api.SetWindowsHookExW(Api.WH_KEYBOARD_LL, hookProc, moduleHandle, 0);
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

        private void OnKeyDown(Keys key) => KeyDownEvent?.Invoke(this, new KeyEventArgs(key), DateTime.Now);

        private void OnKeyUp(Keys key) => KeyUpEvent?.Invoke(this, new KeyEventArgs(key), DateTime.Now);

        // 键盘钩子回调函数
        private int HookProc(int code, int wParam, IntPtr lParam)
        {
            // 如果该消息被丢弃（nCode<0）则不会触发事件
            if (code >= 0)
            {
                Api.TagKBDLLHOOKSTRUCT hookStruct = (Api.TagKBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(Api.TagKBDLLHOOKSTRUCT));
                Keys key = (Keys)hookStruct.vkCode;
                // 按下控键
                if (wParam == Api.WM_KEYDOWN || wParam == Api.WM_SYSKEYDOWN)
                    OnKeyDown(key);
                else if (wParam == Api.WM_KEYUP || wParam == Api.WM_SYSKEYUP)
                    OnKeyUp(key);
            }
            return Api.CallNextHookEx(hookHandle, code, wParam, lParam);
        }

        public void Dispose()=> UnHotKey();
         // 析构函数
        ~KeyboardHook() => Dispose();
    }
}
