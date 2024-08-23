using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace 鹰眼OCR_WPFCore.Service
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Show<T>() where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();
            window.Show();
        }

        public void Hide<T>() where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();
            window.Hide();
        }

        public void Close<T>() where T : Window
        {
            var window = _serviceProvider.GetRequiredService<T>();
            window.Close();
        }

        public T Instance<T>() where T : Window
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
