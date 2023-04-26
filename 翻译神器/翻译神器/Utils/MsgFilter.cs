using System.Windows.Forms;

namespace 翻译神器.Utils
{
    /// <summary>
    /// 线程消息
    /// </summary>
    internal class MsgFilter : IMessageFilter
    {
        /// <summary>
        /// 显示窗口的消息代码
        /// </summary>
        public const uint ShowWindowMsg = 33008u;
        /// <summary>
        /// 显示窗口委托
        /// </summary>
        public delegate void ShowWindowEventHandler();
        /// <summary>
        /// 显示窗口事件
        /// </summary>
        public event ShowWindowEventHandler? ShowWindowEvent;

        /// <summary>
        /// 过滤消息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == ShowWindowMsg)
            {// 显示窗口
                OnShowWindowEvent();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 显示窗口事件
        /// </summary>
        private void OnShowWindowEvent()
        {
            ShowWindowEvent?.Invoke();
        }
    }
}
