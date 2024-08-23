using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace 鹰眼OCR_WPFCore.Models
{
    public partial class OptionViewData : ObservableObject
    {
        /// <summary>
        /// 是否保存截图到本地
        /// </summary>
        [ObservableProperty]
        private bool _isSaveScreenshotToLocal;

        /// <summary>
        /// 是否保存截图到剪切板
        /// </summary>
        [ObservableProperty]
        private bool _isSaveScreenshotToClipboard;

        /// <summary>
        /// 是否自动下载表格
        /// </summary>
        [ObservableProperty]
        private bool _isAutoDownloadTable;

        /// <summary>
        /// 是否保存录音
        /// </summary>
        [ObservableProperty]
        private bool _isSaveRecording;

        /// <summary>
        /// 是否保存语音合成
        /// </summary>
        [ObservableProperty]
        private bool _isSaveSpeechSynthesis;

        /// <summary>
        /// 是否保存拍照
        /// </summary>
        [ObservableProperty]
        private bool _isSavePhoto;

        /// <summary>
        /// 是否识别后添加到末尾
        /// </summary>
        [ObservableProperty]
        private bool _isAddToEndAfterRecognition;

        /// <summary>
        /// 是否识别后复制
        /// </summary>
        [ObservableProperty]
        private bool _isCopyAfterRecognition;

        /// <summary>
        /// 是否识别后翻译
        /// </summary>
        [ObservableProperty]
        private bool _isTranslateAfterRecognition;

        /// <summary>
        /// PDF识别每页延迟（毫秒）
        /// </summary>
        [ObservableProperty]
        private int _pdfRecognitionDelay;

        /// <summary>
        /// 百度Api Key
        /// </summary>
        [ObservableProperty]
        [MaybeNull]
        private string _apiKey;

        /// <summary>
        /// 百度Secret Key
        /// </summary>
        [ObservableProperty]
        [MaybeNull]
        private string _secretKey;

        /// <summary>
        /// 百度翻译AppId
        /// </summary>
        [ObservableProperty]
        [MaybeNull]
        public string _appId;

        /// <summary>
        /// 百度翻译Password
        /// </summary>
        [ObservableProperty]
        [MaybeNull]
        public string _password;
    }
}
