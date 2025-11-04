using System.ComponentModel;
using System.Globalization;

namespace 鹰眼OCR_WPF.Service
{
    public interface ILanguageService
    {
        event PropertyChangedEventHandler? PropertyChanged;
        string SelectedLanguage { get; set; }
        CultureInfo CurrentCulture { get; set; }
        string GetString(string key);
    }
}
