using Tesseract;

namespace 自动进入钉钉直播.ImageRecognition
{
    class TesseractOcr : IDisposable
    {
        private bool _isDisposed;
        public bool IsDisposed { get => _isDisposed; }

        private readonly TesseractEngine engine;
       
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="datapath">数据文件路径（绝对路径）</param>
        /// <param name="language">语言</param>
        public TesseractOcr(string datapath, string language)
        {
            engine = new TesseractEngine(datapath, language, EngineMode.LstmOnly)
            {
                DefaultPageSegMode = PageSegMode.SingleLine// 设为单行识别
            };
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public string GetText(Image img)
        {
            byte[] bArr = ImageToBytes(img);
            using var img1 = Pix.LoadFromMemory(bArr);
            using var page = engine.Process(img1);
            return page.GetText();
        }


        private static byte[] ImageToBytes(Image img)
        {
            using var ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            engine?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~TesseractOcr()
        {
            Dispose();
        }

    }
}
