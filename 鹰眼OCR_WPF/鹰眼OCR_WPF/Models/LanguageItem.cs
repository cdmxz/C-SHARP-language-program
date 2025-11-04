namespace 鹰眼OCR_WPF.Models
{
    public record LanguageItem
    {
        public string DisplayName { get; set; }
        public string CultureName { get; set; }
        public LanguageItem(string displayName, string cultureName)
        {
            DisplayName = displayName;
            CultureName = cultureName;
        }
    }
}
