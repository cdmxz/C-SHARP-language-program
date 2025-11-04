using PDFtoImage;
using System.Drawing;
using System.IO;
using 鹰眼OCR_Extensions.PDF.EventArgss;

namespace 鹰眼OCR_Extensions.PDF
{

    public class PdfToImage
    {
        // 获取完一页的图像时引发事件

        public event EventHandler<GotOnePageEventArgs>? GotOnePageEvent;

        private int _currentIndex = 0;
        private int _total = 0;
        public int CurrentIndex => _currentIndex;
        public int Total => _total;
        private readonly string _base64 = string.Empty;
        public PdfToImage() { }
        public PdfToImage(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("文件不存在");
            }
            var bytes = File.ReadAllBytes(fileName);
            _base64 = Convert.ToBase64String(bytes);
            _total = Conversion.GetPageCount(_base64);
        }

        public Bitmap GetNext()
        {
            if (_currentIndex > _total)
            {
                throw new InvalidOperationException("没有下一页了");
            }
            using MemoryStream s = new();
            Conversion.SavePng(s, _base64, new Index(_currentIndex));
            var img = (Bitmap)Image.FromStream(s);
            _currentIndex++;
            return img;
        }

        public bool HasNext()
        {
            return _currentIndex <= _total;
        }

        /// <summary>
        /// 获取PDF文件每一页的图像
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sleepTime"></param>
        public async Task GetImagesAsync(string fileName, int sleepTime, CancellationToken ct)
        {
            var bytes = File.ReadAllBytes(fileName);
            string base64 = Convert.ToBase64String(bytes);
            int pageCount = Conversion.GetPageCount(base64);

            await Task.Run(() =>
            {
                for (int i = 0; i < pageCount; i++)
                {
                    if (ct.IsCancellationRequested)
                    {
                        return;
                    }

                    using var skBmp = Conversion.ToImage(base64, new Index(i));
                    using MemoryStream s = new();
                    skBmp.Encode(s, SkiaSharp.SKEncodedImageFormat.Png, 100);
                    var img = (Bitmap)Image.FromStream(s);
                    // 获取完一页的图像时引发事件
                    OnGetOnePage(img, i + 1, pageCount);
                    Thread.Sleep(sleepTime);
                }
            }, ct);
        }


        // 获取完一页的图像时引发事件
        private void OnGetOnePage(Bitmap img, int current, int total)
        {
            GotOnePageEvent?.Invoke(this, new GotOnePageEventArgs(img, current, total));
        }
    }

}
