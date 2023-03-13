using System;
using System.Windows.Forms;

namespace 翻译神器.HotKey
{
    /// <summary>
    /// 组合键
    /// </summary>
    public class CombinationKey
    {
        /// <summary>
        /// 组合键
        /// </summary>
        /// <param name="combinationKey">组合键格式：C, Control\C, Control,Shift</param>
        public CombinationKey(string combinationKey)
        {
            if (string.IsNullOrEmpty(combinationKey))
                throw new ArgumentException("参数异常！\r\n传入的组合键格式应为：C, Control或C, Control,Shift");
            string[] arr = combinationKey.Split(',');

            // 解析组合键
            uint fun1 = 0, fun2 = 0;
            // 前一个键为单键（a-z）
            Keys key = (Keys)Enum.Parse(typeof(Keys), arr[0], true);
            if (arr.Length >= 2) // 两个键
                fun1 = HotKeyUtils.GetKeyVal(arr[1]); // 有两个键，则后面第一个键为功能键（ctrl、alt...）
            if (arr.Length == 3) // 三个键
                fun2 = HotKeyUtils.GetKeyVal(arr[2]); // 有三个键，则后面第二个键也为功能键（ctrl、alt...）
            Modifiers = fun1 | fun2;
            Key = key;
            CombinationKeyInfo = combinationKey.Replace(',', '+');
        }


        /// <summary>
        /// 传入的组合键信息
        /// </summary>
        public string CombinationKeyInfo { get; }

        /// <summary>
        /// 修饰键 例如：ctrl,shift,alt...
        /// </summary>
        public uint Modifiers { get; }
        /// <summary>
        /// 普通键 例如：q,w,s,a,d...
        /// </summary>
        public Keys Key { get; }
    }

}
