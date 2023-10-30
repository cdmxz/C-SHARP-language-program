using PDFtoImage;
using System;
using System.Drawing;
using System.IO;

namespace 鹰眼OCR.PDF
{
    public class PdfToImage
    {
        // 获取完一页的图像时引发事件
        public delegate void GetOnePageEventHandler(Image img, int pageNumber);
        public event GetOnePageEventHandler GetOnePageEvent;

        /// <summary>
        /// 获取PDF文件每一页的图像
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sleepTime"></param>
        public void GetImage(string fileName, int sleepTime)
        {
            sleepTime = sleepTime <= 0 ? 100 : sleepTime;
            if (!File.Exists(fileName))
                throw new Exception("文件不存在");
            int pageCount = Conversion.GetPageCount(fileName);
            for (int i = 1; i <= pageCount; i++)
            {
                using var skBmp = Conversion.ToImage(File.ReadAllBytes(fileName), null, i);
                using MemoryStream s = new();
                skBmp.Encode(s, SkiaSharp.SKEncodedImageFormat.Png, 100);
                var img = Image.FromStream(s);
                OnGetOnePage(img, i);// 获取完一页的图像时引发事件
                System.Threading.Thread.Sleep(sleepTime);
            }
        }

        private string ToBase64(string pdfFile)
        {
            byte[] arr = File.ReadAllBytes(pdfFile);
            return Convert.ToBase64String(arr);
        }
        // 获取完一页的图像时引发事件
        private void OnGetOnePage(Image img, int pageNumber) => GetOnePageEvent(img, pageNumber);
    }

}
