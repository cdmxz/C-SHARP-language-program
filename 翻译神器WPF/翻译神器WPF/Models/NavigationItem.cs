using CommunityToolkit.Mvvm.ComponentModel;
using System.Xml.Linq;

namespace 翻译神器WPF.Models
{
    public partial class NavigationItem : ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _targetPage = string.Empty;

        [ObservableProperty]
        private bool _isSelected;

        public NavigationItem(string name, string targetPage)
        {
            Name = name;
            TargetPage = targetPage;
        }
    }
}