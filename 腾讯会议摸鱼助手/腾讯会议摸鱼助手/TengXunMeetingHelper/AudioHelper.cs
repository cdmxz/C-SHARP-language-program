using System.Diagnostics;
using System.Text;
using 腾讯会议摸鱼助手.Utils;
using static System.Windows.Forms.LinkLabel;

namespace 腾讯会议摸鱼助手.TengXunMeetingHelper
{
    internal class AudioHelper
    {
        /// <summary>
        /// 转为单声道音频
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="outFile"></param>
        public static void ConvertToMono(string inFile, string outFile)
        {
            using Process pro = new();
            pro.StartInfo.FileName = "ffmpeg.exe";
            pro.StartInfo.CreateNoWindow = true;
            pro.StartInfo.Arguments = $"-y  -i {inFile}  -acodec pcm_s16le -ac 1 -ar 16000 {outFile}";
            pro.Start();
            pro.WaitForExit(1000);
            pro.Kill();
        }

        /// <summary>
        /// 转为单声道音频
        /// </summary>
        /// <param name="inFile"></param>
        public static void ConvertToMono(string inFile)
        {
            string outFile = Util.GetCurDir() + "\\convert.wav";
            ConvertToMono(inFile, outFile);
            File.Delete(inFile);
            File.Move(outFile, inFile);
        }

        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="wavFile"></param>
        /// <returns></returns>
        public static string Asr(string wavFile)
        {
            using Process pro = new();
            pro.StartInfo.FileName = Util.GetCurDir() + "\\FastASR\\k2_rnnt2_cli.exe";
            pro.StartInfo.Arguments = Util.GetCurDir() + $"\\FastASR\\model\\k2_rnnt2_cli {wavFile}";
            pro.StartInfo.CreateNoWindow = true;
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            string result = string.Empty;
            pro.OutputDataReceived += (_, e) =>
            {
                string? data = e.Data;
                if (data != null && data.StartsWith("Result: \""))
                    result = data.TrimStart("Result: \"".ToCharArray()).TrimEnd("\".".ToCharArray());
            };
            pro.Start();
            pro.BeginOutputReadLine();
            pro.WaitForExit(1500);
            pro.Kill();
            return result;
        }
    }
}
