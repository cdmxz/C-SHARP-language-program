using CommunityToolkit.Mvvm.ComponentModel;
using System.Dynamic;
using 鹰眼OCR_WPF.Service;

namespace 鹰眼OCR_WPF.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected readonly ILanguageService _languageService;

        public dynamic Strings => new LocalizedStrings(_languageService);

        public BaseViewModel(ILanguageService languageService)
        {
            _languageService = languageService;
            _languageService.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Strings));
        }
    }

    public class LocalizedStrings : DynamicObject
    {
        private readonly ILanguageService _languageService;

        public LocalizedStrings(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _languageService.GetString(binder.Name.ToLower());
            return true;
        }
    }
}
