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

        /// <summary>
        /// 获取PDF文件每一页的图像
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sleepTime"></param>
        [Obsolete]
        public void GetImages(string fileName, int sleepTime)
        {
            int pageCount = Conversion.GetPageCount(fileName);
            for (int i = 1; i <= pageCount; i++)
            {
                using var skBmp = Conversion.ToImage(File.ReadAllBytes(fileName), null, i);
                using MemoryStream s = new();
                skBmp.Encode(s, SkiaSharp.SKEncodedImageFormat.Png, 100);
                Bitmap img = (Bitmap)Image.FromStream(s);
                OnGetOnePage(img, i, pageCount);// 获取完一页的图像时引发事件
                Thread.Sleep(sleepTime);
            }
        }

        // 获取完一页的图像时引发事件
        private void OnGetOnePage(Bitmap img, int current, int total)
        {
            GotOnePageEvent?.Invoke(this, new GotOnePageEventArgs(img, current, total));
        }
    }

}
