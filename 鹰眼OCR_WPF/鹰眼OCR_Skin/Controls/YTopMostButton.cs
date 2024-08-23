using System.Windows;
using System.Windows.Controls;

namespace 鹰眼OCR_Skin.Controls
{
    public class TopMostEventArgs(bool isTopMost) : RoutedEventArgs
    {
        public bool IsTopMost { get; set; } = isTopMost;
    }

    // **********************************
    // 自定义控件 顶置按钮
    // **********************************
    public class YTopMostButton : CheckBox
    {
        // 定义 TopChanged 事件
        public event EventHandler<TopMostEventArgs> TopChanged;

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            OnTopChanged(true);
        }


        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnUnchecked(e);
            OnTopChanged(false);
        }

        // 触发 TopChanged 事件
        protected virtual void OnTopChanged(bool isTopMost)
        {
            TopChanged?.Invoke(this, new TopMostEventArgs(isTopMost));
        }
    }
}
