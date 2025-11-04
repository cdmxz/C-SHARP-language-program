using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json.Serialization;

namespace 鹰眼OCR_WPF.Models
{
    /// <summary>
    /// 首页配置数据类
    /// </summary>
    public partial class HomeViewData : ObservableObject
    {
        /// <summary>
        /// 选中的文字识别类型索引
        /// </summary>
        [ObservableProperty]
        private int _selectedOCRTypeIndex;

        /// <summary>
        /// 选中的文字识别语言类型
        /// </summary>
        [ObservableProperty]
        private string _selectedOcrLanguage;

        /// <summary>
        /// 选中的身份证正反面
        /// </summary>
        [ObservableProperty]
        private string _selectedIdCardSide;

        /// <summary>
        /// 选中的语音合成发音人
        /// </summary>
        [ObservableProperty]
        private string _selectedPerson;

        /// <summary>
        /// 语音合成音量
        /// </summary>
        [ObservableProperty]
        private double _sliderSpeed;

        /// <summary>
        /// 选中的翻译源语言
        /// </summary>
        [ObservableProperty]
        private string _selectedSourceLanguage;


        /// <summary>
        /// 选中的翻译目标语言
        /// </summary>
        [ObservableProperty]
        private string _selectedDestLanguage;

        // 不序列化
        [JsonIgnore]
        public bool IsEmpty { get; set; }
    }
}
