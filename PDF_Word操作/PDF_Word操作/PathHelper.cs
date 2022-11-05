using System;
using System.IO;

namespace PDF_Word操作
{
    internal class PathHelper
    {
        public static bool InputPath(bool check, out string path)
        {
            path = Console.ReadLine();
            bool IsFile = File.Exists(path);
            if (IsFile)// 文件存在
                return true;
            else if (check && !Directory.Exists(path))// 不存在则判断是否为文件夹
                throw new ArgumentException("路径不存在！");
            else
                return false;
        }

    }
}
