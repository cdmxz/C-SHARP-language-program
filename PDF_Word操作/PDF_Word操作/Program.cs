using System;

namespace PDF_Word操作
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
               // Console.Clear();
                Console.WriteLine("1、合并PDF（奇数页添加空白页合并）");
                Console.WriteLine("2、合并PDF（不添加空白页合并）");
                Console.WriteLine("3、统计pdf页数");
                Console.WriteLine("4、PDF转图片");
                Console.WriteLine("5、word转pdf");
                Console.WriteLine("6、统计word页数");
                Console.Write("请输入：");
                int num;
                try
                {
                    num = int.Parse(Console.ReadLine());
                    switch (num)
                    {
                        case 1:
                            PdfHelper.MergePdfFile(true);
                            break;
                        case 2:
                            PdfHelper.MergePdfFile(false);
                            break;
                        case 3:
                            PdfHelper.PrintPdfNumber();
                            break;
                        case 4:
                            PdfToImage.ToImage();
                            break;
                        case 5:
                            WordHelper.WordToPdf();
                            break;
                        case 6:
                            WordHelper.PrintWordNumber();
                            break;
                        case 0:
                            Console.Clear();
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }
        }
    }
}
