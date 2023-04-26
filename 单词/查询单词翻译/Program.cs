using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Linq;
using QueryWordLib.DocumentHelper;
using QueryWordLib.WordHelper;

namespace 查询单词翻译
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string path;
            if (args.Length == 0)
            {
                Console.Write("请输入文件路径：");
                path = Console.ReadLine()?.Trim("\"".ToCharArray());
            }
            else
            {
                path = args[0];
            }
            if (path == null || !File.Exists(path))
            {
                Console.WriteLine("文件不存在！\r\n按任意键退出...");
                Console.ReadKey();
                return;
            }
            string outFile = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_译文";
            var wordInfos = Word.Query(path);
            try
            {
                WordDocumentHelper.ExportToWordDocument(wordInfos, outFile + ".docx");
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法导出Word文档！原因：" + ex.Message);
                Console.WriteLine("\r\n\r\n\t即将导出为txt文档");
                TextHelper.ExportToTextDocument(wordInfos, outFile + ".txt");
            }

            Console.WriteLine("完成\r\n按任意键退出...");
            Console.ReadLine();
        }        
    }
}