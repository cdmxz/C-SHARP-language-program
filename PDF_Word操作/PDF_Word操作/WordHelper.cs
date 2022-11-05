using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Linq;

namespace PDF_Word操作
{
    internal class WordHelper
    {
        private readonly static string[] exts = new string[] { ".doc", ".docx" };

        public static void WordToPdf()
        {
            Console.WriteLine("\n请输入源文件路径：");
            bool sourceIsFile = PathHelper.InputPath(true, out string source);
            if (sourceIsFile)
            {// 只有单个文件
                string newFile = Path.ChangeExtension(source, "pdf");
                Console.WriteLine($"正在保存：{newFile}");
                ToPdf(source, newFile);
                return;
            }
            Console.WriteLine("\n请输入保存路径：");
            string dest = Console.ReadLine();
            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            var files = exts.SelectMany(ext => Directory.EnumerateFiles(source, "*", SearchOption.AllDirectories));
            int total = 0;
            foreach (string file in files)
            {
                string newFile = dest + "\\" + Path.GetFileNameWithoutExtension(file) + ".pdf";
                Console.WriteLine($"正在保存：{newFile}");
                ToPdf(source, newFile);
                total++;
            }

            Console.WriteLine($"\n总计：{total}个文件");
            Console.ReadKey();
        }

        private static void ToPdf(string source, string dest)
        {
            Application application = new Application();
            Document document = application.Documents.Open(source);
            document.ExportAsFixedFormat(dest, WdExportFormat.wdExportFormatPDF);
            document.Close();
            application.Quit();
        }

        public static void PrintWordNumber()
        {
            Console.WriteLine("\n请输入源文件路径：");
            bool sourceIsFile = PathHelper.InputPath(true, out string path);
            if(sourceIsFile)
            {
                Console.WriteLine($"{Path.GetFileName(path)} {GetWordNumber(path)}页");
                return;
            }
            string[] files = Directory.GetFiles(path, "*.doc", SearchOption.AllDirectories);
            int total = 0;
            foreach (string file in files)
            {
                int page = GetWordNumber(file);
                Console.WriteLine($"{Path.GetFileName(file)} {page}页");
                total += page;
            }
            Console.WriteLine($"\n总计：{total}页");
            Console.ReadKey();
        }

        public static int GetWordNumber(string file)
        {
            try
            {
                Application application = new Application();
                Document document = application.Documents.Open(file);
                int pageCount = document.ComputeStatistics(WdStatistic.wdStatisticPages);
                application.Quit();
                document.Close();
                return pageCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"打开{file}失败！\n原因：{ex.Message}");
                return 0;
            }
        }
    }
}
