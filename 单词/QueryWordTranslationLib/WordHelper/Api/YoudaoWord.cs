using AngleSharp.Html.Parser;
using System;
using System.Net.Http;
using System.Text;
using System.Web;

namespace QueryWordLib.WordHelper.Api
{
    /// <summary>
    /// 有道接口
    /// </summary>
    public class YoudaoWord
    {
        private static readonly string URL = "https://www.youdao.com/result";

        /// <summary>
        /// 查询单词音标、翻译
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static WordInfo QueryExplanation(string word)
        {
            string html = Send(word);
            return ParseHtml(word, html);
        }


        /// <summary>
        /// 解析html页面
        /// 单词名称不从html页面提取（避免提取不到）
        /// </summary>
        /// <param name="word"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        private static WordInfo ParseHtml(string word, string html)
        {
            //创建解析器
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            // 获取单词
            //string word = document.QuerySelector("div[class=\"title\"]")?.TextContent ?? "无法获取单词";
            //word = word.TrimEnd("语速0.5x1.0X1.5X发音英式发音美式发音".ToCharArray());
            // 获取美式音标
            var spans = document.QuerySelectorAll("span[class=\"phonetic\"]");
            string phonetic = "";
            // 正常情况下有两个span元素（一个英式音标，一个美式英标）
            if (spans.Length > 1)// 避免音标不存在
                phonetic = spans[spans.Length - 1].TextContent;
            // 获取单词翻译
            var lis = document.QuerySelectorAll("li[class=\"word-exp\"]");
            StringBuilder sb = new StringBuilder();
            foreach (var item in lis)
            {
                spans = item?.QuerySelectorAll("span");
                if (spans == null)
                {
                    sb.Append("无法获取单词翻译");
                    break;
                }
                foreach (var sp in spans)
                {
                    sb.Append(sp.TextContent);
                }
                sb.Append("\t");
                //sb.Append(Environment.NewLine);
            }
            // 删除末尾的换行符
            //var explnation = sb.ToString().TrimEnd(Environment.NewLine.ToCharArray());
            var explnation = sb.ToString().TrimEnd('\t');

            return new WordInfo(word, phonetic, explnation);
        }

        /// <summary>
        /// 获取html页面
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static string Send(string word)
        {
            string url = CombinUrl(word);
            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.52");
                    using (HttpResponseMessage response = client.SendAsync(request).Result)
                        return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        /// <summary>
        /// 拼接请求URL
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static string CombinUrl(string word) => $"{URL}?word={HttpUtility.UrlEncode(word, Encoding.UTF8)}&lang=en";
    }
}
