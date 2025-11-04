using System;
using 翻译神器WPF.TranslationApi;
using 翻译神器WPF.TranslationApi.Implements;
using 翻译神器WPF.TranslationApis.ApiCommon;
using 翻译神器WPF.TranslationApi.Common;

namespace 翻译神器WPF.TranslationApis
{
    public class ApiFactory
    {
        // 加载翻译API，支持注入密钥（兼容Baidu/Youdao/Tengxun）
        public static ITranslateApi Create(
            string? apiChineseName = null,
            string? apiCode = null,
            BaiduKey? baiduKey = null,
            YoudaoKey? youdaoKey = null,
            TengxunKey? tengxunKey = null)
        {
            ApiCodes? code = null;
            if (apiChineseName != null) 
                code = ApiNames.GetApiCodeByApiChineseName(apiChineseName);
            if (apiCode != null) 
                code = Enum.Parse<ApiCodes>(apiCode);

            return code switch
            {
                ApiCodes.Baidu => new Baidu(baiduKey ?? new BaiduKey()),
                ApiCodes.YouDao => new Youdao(youdaoKey ?? new YoudaoKey()),
                ApiCodes.TengXun => new Tengxun(tengxunKey ?? new TengxunKey()),
                _ => throw new ArgumentException(" 参数不正确！"),
            };
        }
    }
}
