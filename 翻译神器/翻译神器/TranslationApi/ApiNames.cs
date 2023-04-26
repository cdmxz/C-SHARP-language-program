using System;
using System.Collections.Generic;
using System.Linq;

namespace 翻译神器.TranslationApi
{
    /// <summary>
    /// 程序使用的所有翻译Api 的中文名称 对应的代码
    /// </summary>
    public enum ApiCode
    {
        Baidu,
        YouDao,
        TexngXun
    }

    public class ApiNames
    {
        /// <summary>
        /// 程序使用的翻译Api的中文名称
        /// </summary>
        private static readonly Dictionary<ApiCode, string> apiChineseNameDict = new()
        {
            //
            // 修改时别忘了改 LoadTranslateApi方法
            //
            {ApiCode.Baidu ,"百度翻译" },
            {ApiCode.YouDao,"有道翻译" },
            {ApiCode.TexngXun, "腾讯翻译"},
        };

        public static string GetApiChineseNameByApiCode(ApiCode code)
        {
            if (!apiChineseNameDict.ContainsKey(code))
                throw new ArgumentException("不存在该ApiCode！");
            return apiChineseNameDict[code];
        }

        public static ApiCode GetApiCodeByApiChineseName(string chineseName)
        {
            if (!apiChineseNameDict.ContainsValue(chineseName))
                throw new ArgumentException("不存在该ChineseName！");
            // 因为一个ApiCode 只对应一个 ApiChineseName
            // 所以不必担心ApiChineseName会重复
            return apiChineseNameDict.FirstOrDefault(x => x.Value.Equals(chineseName, StringComparison.CurrentCultureIgnoreCase)).Key;
        }

        /// <summary>
        /// 获取所有翻译Api的中文名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetApiChineseNames()
        {
            return apiChineseNameDict.Values.ToArray();
        }

    }
}
