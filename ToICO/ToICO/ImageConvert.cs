using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ToICO
{
    class ImageConvert
    {
        /// <summary>
        /// 调整图像大小，会释放原图资源
        /// </summary>
        /// <param name="oldBmp">要调整大小的图像</param>
        /// <param name="newSize">目标大小</param>
        /// <returns>改变大小后的图像</returns>
        private static Bitmap ResizeImage(Bitmap oldBmp, Size newSize)
        {
            if (oldBmp == null)
                throw new Exception("函数名：ResizeImage\n原因：参数oldBmp为空！");

            Bitmap newBmp = new Bitmap(newSize.Width, newSize.Height);
            using Graphics g = Graphics.FromImage(newBmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;// 高质量
            g.DrawImage(oldBmp, new Rectangle(0, 0, newSize.Width, newSize.Height), new Rectangle(0, 0, oldBmp.Width, oldBmp.Height), GraphicsUnit.Pixel);
            oldBmp.Dispose();
            return newBmp;
        }

        /// <summary>
        /// 将图片转为ico
        /// </summary>
        /// <param name="source">源图片路径</param>
        /// <param name="dest">目标图片路径</param>
        /// <param name="destSize">转换成ico后的大小</param>
        public static void ConvertToIcon(string source, string dest, Size destSize)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(dest))
                return;

            Bitmap bit;
            if (destSize.Width <= 0 && destSize.Height <= 0)     // 是否改变原图大小 
                bit = new Bitmap(source);
            else
                bit = ResizeImage(new Bitmap(source), destSize);// 改变原图大小

            // 将图片转换为图标（转换为含有一张png图片的icon文件）
            using MemoryStream msImg = new MemoryStream(), msIco = new MemoryStream();
            // 将原图以png格式保存到流中
            bit.Save(msImg, ImageFormat.Png);
            using BinaryWriter bin = new BinaryWriter(msIco);
            // 将ico图标的头部写到流中
            // 写文件头（6字节）
            bin.Write((short)0);           // 0-1保留字节
            bin.Write((short)1);           // 2-3文件类型。1=图标, 2=光标
            bin.Write((short)1);           // 4-5图像数量（图标可以包含多个图像）
                                           // 写图像信息块（16字节）
            bin.Write((byte)bit.Width);    // 6图标宽度
            bin.Write((byte)bit.Height);   // 7图标高度
            bin.Write((byte)0);            // 8颜色数（2＝单色，0>=256色。若像素位深>=8，填0。）
            bin.Write((byte)0);            // 9保留。必须为0
            bin.Write((short)0);           // 10-11调色板
            bin.Write((short)32);          // 12-13位深
            bin.Write((int)msImg.Length);  // 14-17位图数据大小
            bin.Write(22);                 // 18-21位图数据起始字节

            // 将png图像的数据写到流中
            bin.Write(msImg.ToArray());
            bin.Flush();
            bin.Seek(0, SeekOrigin.Begin);

            // 将流写到图标文件
            using FileStream fs = new FileStream(dest, FileMode.Create);
            fs.Write(msIco.ToArray(), 0, msIco.ToArray().Length);
        }

        public static void ConvertImage(string source, string dest, Size destSize, string destFormat)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(dest))
                return;
            if (destFormat.ToLower() == "ico")
            {
                ConvertToIcon(source, dest, destSize);
                return;
            }
            ImageFormat format = StrToImageFormat(destFormat);
            if (format == null)
                throw new Exception("目标格式错误！");
            Bitmap bit = (destSize.Width <= 0 && destSize.Height <= 0) ? new Bitmap(source) : ResizeImage(new Bitmap(source), destSize);
            // 将原图以指定格式格式保存
            bit.Save(dest, format);
            bit.Dispose();
        }

        private static ImageFormat StrToImageFormat(string format)
        {
            switch (format.ToLower())
            {
                case "png":
                    return ImageFormat.Png;
                case "jpg":
                    return ImageFormat.Jpeg;
                case "jpeg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                case "gif":
                    return ImageFormat.Gif;
                case "ico":
                    return ImageFormat.Icon;
                case "tiff":
                    return ImageFormat.Tiff;
                case "wmf":
                    return ImageFormat.Wmf;
                default:
                    return null;
            }
        }
    }
}
