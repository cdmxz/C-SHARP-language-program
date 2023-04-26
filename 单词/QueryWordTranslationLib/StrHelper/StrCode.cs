namespace QueryWordLib.StrHelper
{
    public class StrCode
    {
        /// <summary>
        /// 判断一个字符串是否包含中文字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool HaveChinese(string str)
        {
            foreach (var ch in str)
            {
                if (ch < 0 || ch > 127)
                    return true;
            }
            return false;
        }
    }
}
