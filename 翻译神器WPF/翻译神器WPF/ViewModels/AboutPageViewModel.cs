using CommunityToolkit.Mvvm.ComponentModel;
using System.Reflection;

namespace 翻译神器WPF.ViewModels
{
    public partial class AboutPageViewModel : ObservableObject
    {
        public AboutPageViewModel()
        {
            Environment.Version.ToString();
            var ver = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            Title = "翻译神器 v" + ver?[..^4];
        }
      
        [ObservableProperty]
        private string _title;

      
    }
}
