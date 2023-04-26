using QueryWordLib.StrHelper;
using QueryWordLib.WordHelper.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QueryWordLib.WordHelper
{
    public class Word
    {
        /// <summary>
        /// 查询单词翻译
        /// </summary>
        /// <param name="inFile"></param>
        /// <returns></returns>
        public static WordInfo[] Query(string inFile)
        {
            List<WordInfo> list = new List<WordInfo>();
            string[] lines = File.ReadAllLines(inFile).Where((line) => !string.IsNullOrEmpty(line)).ToArray();
            string result;
            foreach (string line in lines)
            {
                if (StrCode.HaveChinese(line))
                { // 如果一行单词里面含有中文则表示该行单词已经含有翻译，则不翻译直接输出到文件
                    list.Add(WordInfo.Parse(line));
                    result = line;
                }
                else
                {
                    var tmp = YoudaoWord.QueryExplanation(line);
                    result = tmp.ToString();
                    list.Add(tmp);
                }
                Console.WriteLine(result + Environment.NewLine);
            }
            return list.ToArray();
        }


        /// <summary>
        /// 查询单词翻译
        /// </summary>
        /// <param name="inFile"></param>
        /// <returns></returns>
        public static async Task<WordInfo[]> QueryAsync(string inFile, CancellationToken token)
        {
            List<WordInfo> list = new List<WordInfo>();
            StreamReader sr = new StreamReader(inFile, System.Text.Encoding.UTF8);
            await Task.Run(new Action(() =>
             {
                 string line;
                 while (!token.IsCancellationRequested && (line = sr.ReadLine()) != null)
                 {
                     if (string.IsNullOrEmpty(line))
                         continue;
                     if (StrCode.HaveChinese(line))
                     { // 如果一行单词里面含有中文则表示该行单词已经含有翻译，则不翻译直接输出到文件
                         list.Add(WordInfo.Parse(line));
                     }
                     else
                     {
                         var result = YoudaoWord.QueryExplanation(line);
                         list.Add(result);
                     }
                 }
             }));
            sr.Close();
            return list.ToArray();
        }
    }
}
