using QueryWordLib.WordHelper;
using System.IO;

namespace QueryWordLib.DocumentHelper
{
    public class TextHelper
    {
        public static void ExportToTextDocument(WordInfo[] wordInfo, string outPath)
        {
            FileStream fs = new FileStream(outPath,FileMode.Create);
            StreamWriter sw = new StreamWriter(fs); 

            // 向text添加内容
            foreach (var item in wordInfo)
            {
                sw.WriteLine(item.Word);
                // 避免音标为空而在word中增加一行空行
                string str = string.IsNullOrEmpty(item.Phonetic) ? "" : item.Phonetic + "\r\n";
                sw.WriteLine(str + item.Explanation + "\r\n");
            }
            sw.Close();
            fs.Close();
        }
    }
}
