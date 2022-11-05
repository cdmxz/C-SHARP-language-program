using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PDF_Word操作
{
    internal class PdfHelper
    {
        public static void PrintPdfNumber()
        {
            Console.WriteLine("请输入源文件路径：");
            bool sourceIsFile = PathHelper.InputPath(true, out string source);
            if (sourceIsFile)
            {
                Console.WriteLine($"{Path.GetFileName(source)} {GetPdfNumber(source)}页");
                return;
            }
            string[] files = Directory.GetFiles(source, "*.pdf", SearchOption.AllDirectories);
            int total = 0;
            foreach (string file in files)
            {
                int number = GetPdfNumber(file);
                Console.WriteLine($"{Path.GetFileName(file)} {number}页");
                total += number;
            }
            Console.WriteLine($"总计：{total}页");
            Console.ReadKey();
        }

        public static int GetPdfNumber(string file)
        {
            PdfReader reader = new PdfReader(file);
            PdfDocument doc = new PdfDocument(reader);
            int page = doc.GetNumberOfPages();
            reader.Close();
            doc.Close();
            return page;
        }

        public static void MergePdfFile(bool addBlank)
        {
            Console.WriteLine("请输入源文件文件夹：");
            string source = Console.ReadLine();
            Console.WriteLine("请输入保存名称：");
            string dest = Console.ReadLine();
            string[] files = FileSort(Directory.GetFiles(source, "*.pdf"));
            MergePdfFiles(files, dest, addBlank);
            Console.WriteLine("成功");
            Console.ReadKey();
        }


        //文件名排序
        public static string[] FileSort(string[] files)
        {
            string temp;
            for (int i = 0; i < files.Length - 1; i++)
            {
                for (int j = 0; j < files.Length - 1 - i; j++)
                {
                    if (CustomSort(Path.GetFileName(files[j]), Path.GetFileName(files[j + 1])))
                    {
                        temp = files[j];
                        files[j] = files[j + 1];
                        files[j + 1] = temp;
                    }
                }
            }
            return files;
        }

        public static bool CustomSort(string str1, string str2)
        {
            string tmp = Regex.Replace(str1, @"[^0-9]+", "");
            if (string.IsNullOrEmpty(tmp))
                return false;
            int result1 = Convert.ToInt32(tmp);
            tmp = Regex.Replace(str2, @"[^0-9]+", "");
            if (string.IsNullOrEmpty(tmp))
                return false;
            int result2 = Convert.ToInt32(tmp);
            if (result1 > result2)
                return true;
            else
                return false;
        }

        public static void MergePdfFiles(string[] files, string outMergeFile, bool addBlank)
        {
            if (files.Length == 0)
                return;
            PdfDocument destDoc = new PdfDocument(new PdfWriter(outMergeFile));
            PdfMerger merger = new PdfMerger(destDoc);
            foreach (var file in files)
            {
                Console.WriteLine($"正在合并：{Path.GetFileName(file)}");
                PdfDocument doc = new PdfDocument(new PdfReader(file));
                int pageNum = doc.GetNumberOfPages();
                merger.Merge(doc, 1, pageNum);
                // 奇数页页末添加空白页                 
                if (addBlank && pageNum % 2 == 1)
                {
                    destDoc.AddNewPage();
                }
                doc.Close();
            }
            merger.Close();
            destDoc.Close();
        }
    }
}
