using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace 翻译神器.TranslationApi.Common
{
    public class TranslateUtil
    {
        /// <summary>
        ///  Image转Base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns>转换后的base64数据</returns>
        public static string ImageToBase64(Image img)
        {
            using MemoryStream ms = new();
            img.Save(ms, ImageFormat.Png);
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string GetLowerCaseMd5(string str)
        {
            MD5 md5 = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new();
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static string GetUpperCaseMd5(string str)
        {
            return GetLowerCaseMd5(str).ToUpper();
        }

        public static string GetLowerCaseMd5(Image img)
        {
            MD5 md5 = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5.ComputeHash(ImageToBytes(img));
            StringBuilder sb = new();
            foreach (byte b in data)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        public static byte[] ImageToBytes(Image img)
        {
            using MemoryStream ms = new();
            img.Save(ms, ImageFormat.Png);// 将图像写入内存流
            return ms.GetBuffer();
        }

    }
}
