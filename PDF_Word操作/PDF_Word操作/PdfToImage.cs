using PdfiumViewer;
using System;
using System.Drawing;
using System.IO;

namespace PDF_Word操作
{
    internal class PdfToImage
    {
        public static void ToImage()
        {
            Console.WriteLine("请输入源文件路径：");
            bool sourceIsFile = PathHelper.InputPath(true, out string source);
            if (sourceIsFile)
            {
                string savePath = Path.ChangeExtension(source, "").TrimEnd('.');
                if (Directory.Exists(savePath))
                    Directory.Delete(savePath, true);
                Directory.CreateDirectory(savePath);
                using (PdfDocument pdf = PdfiumViewer.PdfDocument.Load(source))
                {
                    for (int i = 1; i <= pdf.PageCount; i++)
                    {
                        using (Image img = GetPdfImage(pdf, i))
                            img.Save($"{savePath}\\_{i}.png", System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }

        /// <summary>
        /// 获取pdf文件第n页的图片
        /// </summary>
        /// <param name="pdf"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        private static Image GetPdfImage(PdfDocument pdf, int pageNumber)
        {
            int w = (int)pdf.PageSizes[pageNumber - 1].Width;
            int h = (int)pdf.PageSizes[pageNumber - 1].Height;
            return pdf.Render(pageNumber - 1, w, h, 300, 300, PdfRenderFlags.CorrectFromDpi);
        }

    }
}
