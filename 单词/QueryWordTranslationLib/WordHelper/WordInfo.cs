using System;
using System.Linq;

namespace QueryWordLib.WordHelper
{
    /// <summary>
    /// 单词信息
    /// </summary>
    public class WordInfo
    {
        /// <summary>
        /// 单词
        /// </summary>
        public string Word { get; private set; }

        /// <summary>
        /// 音标
        /// </summary>
        public string Phonetic { get; private set; }

        /// <summary>
        /// 解释
        /// </summary>
        public string Explanation { get; private set; }

        public WordInfo(string word, string phonetic, string explanation)
        {
            Word = word;
            Phonetic = phonetic;
            Explanation = explanation;
        }


        /// <summary>
        /// 字符串格式：
        /// line /laɪn/ 行；线；线条 
        /// 或
        /// line 行；线；线条 
        /// </summary>
        /// <param name="str"></param>
        public static WordInfo Parse(string str)
        {
            var arr = str.Split(' ');
            var arr1 = arr.Where(item => !string.IsNullOrEmpty(item)).ToArray();
            
            string explanation = arr1.Length > 2 ? arr[2] : arr[1];
            string phonetic = arr1.Length > 2 ? arr[1] : "";
            return new WordInfo(arr1[0], phonetic, explanation);           
        }

        public override string ToString()
        {
            return Word + Environment.NewLine + Phonetic + Environment.NewLine + Explanation;
        }
    }
}
