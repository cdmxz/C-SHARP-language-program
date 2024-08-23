using System.Windows;

namespace 鹰眼OCR_WPFCore.Service
{
    /// <summary>
    /// 窗体服务，用于显示、隐藏、关闭窗体
    /// </summary>
    public interface IWindowService
    {
        void Show<T>() where T : Window;
        void Hide<T>() where T : Window;
        void Close<T>() where T : Window;
        T Instance<T>() where T : Window;
    }
}
