using 翻译神器WPF.TranslationApis.ApiCommon;

namespace 翻译神器WPF.TranslationApi
{

    public class ApiNames
    {
        /// <summary>
        /// 程序使用的翻译Api的中文名称
        /// </summary>
        private static readonly Dictionary<ApiCodes, string> apiChineseNameDict = new()
        {
            //
            // 修改时别忘了改 LoadTranslateApi方法
            //
            {ApiCodes.Baidu ,"百度翻译" },
            {ApiCodes.YouDao,"有道翻译" },
            {ApiCodes.TengXun, "腾讯翻译"},
        };

        public static string GetApiChineseNameByApiCode(ApiCodes code)
        {
            if (!apiChineseNameDict.ContainsKey(code))
                throw new ArgumentException("不存在该ApiCode！");
            return apiChineseNameDict[code];
        }

        public static ApiCodes GetApiCodeByApiChineseName(string chineseName)
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
        public static string[] GetAllApiChineseNames()
        {
            return [.. apiChineseNameDict.Values];
        }

    }
}
