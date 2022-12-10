using System.Collections.Generic;
using System.Drawing;

namespace 翻译神器.TranslationApi
{

    /// <summary>
    /// 翻译Api类遵循的接口
    /// </summary>
    public interface ITranslateApi
    {
        /// <summary>
        /// 文本翻译
        /// </summary>
        /// <param name="srcText">待翻译的文本</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        public void TextTranslate(string srcText, string from, string to, out string src_text, out string dst_text);
        /// <summary>
        /// 图片翻译
        /// </summary>
        /// <param name="img">待翻译的图片</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        public void PictureTranslate(Image img, string from, string to, out string src_text, out string dst_text);

        /// <summary>
        /// 测试密钥是否有效
        /// 测试失败则抛出异常
        /// </summary>
        /// <exception cref="Exception">测试失败则抛出异常</exception>
        public void KeyTest();

        /// <summary>
        /// 翻译Api支持的语言类型
        /// </summary>
        public Dictionary<string, string> LangDict { get; }

        /// <summary>
        /// 翻译Api的中文名称
        /// </summary>
        public string ApiChineseName { get; }

        /// <summary>
        /// 获取 支持的 语言代码
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedLanguageCode();

        /// <summary>
        /// 获取支持的 目标 语言名称
        /// </summary>
        /// <returns></returns>
        public string[] GetSupportedTargetLanguageNames();

        /// <summary>
        /// 获取 默认 语言代码
        /// </summary>
        /// <returns></returns>
        public string GetDefaultLangCode();



    }
}
