using System.IO;

namespace 鹰眼OCR_WPFCore.Helper
{
    internal class PathHelper
    {
        /// <summary>
        /// 搜索指定目录下的所有图片
        /// 包括子目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetImages(string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            var images = files.Where(IsImageExt);
            return images.ToArray();
        }

        /// <summary>
        /// 给定的扩展名是否是图片
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsImageExt(string ext)
        {
            string s = ext.ToLower();
            return s.EndsWith("png") || s.EndsWith("jpg") || s.EndsWith("bmp") || s.EndsWith("webp") || s.EndsWith("jpeg");
        }

    }
}
