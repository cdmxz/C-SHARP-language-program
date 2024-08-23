using OpenCvSharp;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace 鹰眼OCR_WPFCore.Helper
{
    internal class BitmapHelper
    {
        /// <summary>
        /// 保存图片到指定目录
        /// 文件名格式：宽x高_当前时间.png
        /// </summary>
        /// <param name="img"></param>
        /// <param name="dir"></param>
        public static void SaveImage(Bitmap img, string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string path = $"{dir}{img.Width}x{img.Height}_{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.png";
            img.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void SaveToClipboard(Bitmap bitmap)
        {
            try
            {
                Clipboard.SetData(DataFormats.Bitmap, bitmap);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 将opencv的Mat转为BitmapImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static BitmapImage MatToBitmapImage(Mat image)
        {
            using Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
            BitmapImage result = BitmapToBitmapImage(bitmap);
            return result;
        }

        public static Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {

            using MemoryStream stream = new MemoryStream();

            // 坑点：格式选Bmp时，不带透明度
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            stream.Position = 0;

            BitmapImage result = new BitmapImage();
            result.BeginInit();
            // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
            // Force the bitmap to load right now so we can dispose the stream.
            result.CacheOption = BitmapCacheOption.OnLoad;
            result.StreamSource = stream;
            result.EndInit();
            result.Freeze();
            return result;
        }


        /// <summary>
        /// 将文件转为Image
        /// </summary>
        /// <param name="fileName"></param>
        public static Bitmap BitmapFromFile(string fileName)
        {
            using Image img = Image.FromFile(fileName);
            return new Bitmap(img);// 防止文件被锁
        }


        public static System.Drawing.Bitmap ImageSourceToBitmap(ImageSource imageSource)
        {
            BitmapSource m = (BitmapSource)imageSource;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb); // 坑点：选Format32bppRgb将不带透明度

            System.Drawing.Imaging.BitmapData data = bmp.LockBits(
            new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

            m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
            bmp.UnlockBits(data);

            return bmp;
        }
    }
}
