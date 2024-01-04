using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace 腾讯会议摸鱼助手.Utils
{
    internal static class Util
    {
        /// <summary>
        /// Point转换为string 不带括号
        /// 转换前 {x=1,y=1}
        /// 转换后 1,1
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static string PointToStringWithoutBracket(this Point p)
        {
            var collect = MatchNumbers(p.ToString());
            return $"{collect[0]},{collect[1]}";
        }

        /// <summary>
        /// 通过pointStr，sizeStr转Rectangle
        /// </summary>
        /// <param name="pointStr">格式：{X=13,Y=-43}</param>
        /// <param name="sizeStr">格式：{Width=293,Height=80}</param>
        /// <returns></returns>
        public static Rectangle RectangleParse(string pointStr, string sizeStr)
        {
            Size size = SizeParse(sizeStr);
            Point point = PointParse(pointStr);
            return new Rectangle(point, size);
        }

        /// <summary>
        /// str转size
        /// 输入{Width=293,Height=80}
        /// </summary>
        /// <param name="sizeStr"></param>
        /// <returns></returns>
        public static Size SizeParse(string sizeStr)
        {
            if (string.IsNullOrEmpty(sizeStr))
                return new Size();
            var col = Split(sizeStr);
            int width = int.Parse(col[0]);
            int height = int.Parse(col[1]);
            return new Size(width, height);
        }

        /// <summary>
        /// 输入{X=13,Y=-43}
        /// str转point
        /// </summary>
        /// <param name="pointStr"></param>
        /// <returns></returns>
        public static Point PointParse(string pointStr)
        {
            if (string.IsNullOrEmpty(pointStr))
                return new Point();
            var col = Split(pointStr);
            int x = int.Parse(col[0]);
            int y = int.Parse(col[1]);
            return new Point(x, y);
        }

        /// <summary>
        /// 正则表达式匹配数字
        /// 输入{X=13,Y=-43,Width=293,Height=80}
        /// 输出集合：13,-43,293,80
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static MatchCollection MatchNumbers(string input)
        {
            Regex re = new("(-)*\\d+(,\\d+)*");
            if (!re.IsMatch(input))
                throw new ArgumentException("参数异常！" + nameof(input));
            return re.Matches(input);
        }

        /// <summary>
        /// 分割字符串，通过中文/英文 逗号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string[] Split(string input)
        {
            input = input.Replace("，", ",");
            return input.Split(",");
        }

        /// <summary>
        /// 获取当前exe所在的目录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetCurDir()
        {
            string? dir = Path.GetDirectoryName(Environment.ProcessPath);
            if (dir is null)
                throw new Exception("无法获取程序目录！");
            return dir;
        }

        /// <summary>
        /// 获取临时wav文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetTempWavFilePath()
        {
            StringBuilder sb = new();
            Random r = new();
            for (int i = 0; i < 6; i++)
            {
                char letter = (char)r.Next(48, 122);
                if (char.IsLetterOrDigit(letter))
                    sb.Append(letter);
            }
            return $"{GetTempWavFileDir()}\\{sb}.wav";
        }

        /// <summary>
        /// 获取临时wav文件文件夹
        /// </summary>
        /// <returns></returns>
        public static string GetTempWavFileDir()
        {
            return $"{GetCurDir()}\\TempWavFile";
        }

        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            using Process pro = new Process();
            pro.StartInfo.FileName = "explorer.exe";
            pro.StartInfo.Arguments = url;
            pro.Start();
        }
    }
}
