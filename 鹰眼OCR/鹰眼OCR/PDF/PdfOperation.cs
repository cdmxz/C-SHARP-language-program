using PdfiumViewer;
using System;
using System.Drawing;
using System.IO;

namespace 鹰眼OCR.PDF
{
    class PdfOperation
    {
        public static bool IsPDFile(string fileName)
        {
            if (!File.Exists(fileName))
                return false;
            using (StreamReader sr = new StreamReader(fileName))
                return sr.ReadLine().ToLower().IndexOf("pdf") != -1;
        }
    }

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
            using (PdfDocument pdf = PdfDocument.Load(fileName))
            {
                for (int i = 1; i <= pdf.PageCount; i++)
                {
                    using (Image img = GetPdfImage(pdf, i))
                        OnGetOnePage(img, i);// 获取完一页的图像时引发事件
                    System.Threading.Thread.Sleep(sleepTime);
                }
            }
        }

        // 获取完一页的图像时引发事件
        private void OnGetOnePage(Image img, int pageNumber) => GetOnePageEvent(img, pageNumber);

        /// <summary>
        /// 获取pdf文件第n页的图片
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        private Image GetPdfImage(PdfDocument pdf, int pageNumber)
        {
            int w = (int)pdf.PageSizes[pageNumber - 1].Width;
            int h = (int)pdf.PageSizes[pageNumber - 1].Height;
            return pdf.Render(pageNumber - 1, w, h, 300, 300, PdfRenderFlags.Annotations);
        }
    }
}