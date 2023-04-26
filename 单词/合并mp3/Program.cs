using NAudio.Wave;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using QueryWordLib.StrHelper;

namespace 合并mp3
{
    internal class Program
    {
        static HttpClient httpClient = new();
        static void Main(string[] args)
        {
            string file;
            string[] audios;
            string audioDir;
            string dest;
            string lrcFile;
            if (args.Length >= 1 && args.Length <= 2)
            {
                file = args[0];
                audioDir = GetDownloadDir();
                if (Directory.Exists(audioDir))
                    Directory.Delete(audioDir, true);
                Directory.CreateDirectory(audioDir);
                dest = DeleteExtension(file) + ".mp3";
                string[] words = ReadTxt(file);
                WordInfo[] wordInfos = QueryWordInfos(words, out bool haveError);
                if (haveError)
                {
                    OutPut("是否继续下载单词音频？(Enter)", ConsoleColor.DarkYellow);
                    Console.ReadLine();
                }
                DownloadAllAudio(wordInfos, audioDir);
            }
            else if (args.Length == 3 && args[0] == "h")
            {
                Console.WriteLine("合并mp3\n路径：" + args[1]);
                audioDir = args[1];
                dest = args[2];
            }
            else
            {
                Console.WriteLine("参数有误！");
                httpClient.Dispose();
                return;
            }
            audios = Directory.GetFiles(audioDir, "*.mp3");
            SortAsFileCreatTime(ref audios);
            Combine(audios, dest);
            lrcFile = DeleteExtension(dest) + ".lrc";
            CreateLyrics(audios, lrcFile);
            //Console.WriteLine($"输出文件总长度：{new Mp3File(dest).Duration:N2}秒");
            Console.Read();
        }

        public static string DeleteExtension(string fileName)
        {
            int index = fileName.LastIndexOf(".");
            if (index == -1)
                return fileName;
            return fileName.Substring(0, index);
        }

        public static void Combine(string[] inputFiles, string outFile)
        {
            if (File.Exists(outFile))
                File.Delete(outFile);
            using FileStream output = new(outFile, FileMode.CreateNew);
            foreach (string file in inputFiles)
            {
                Console.WriteLine("合并：" + Path.GetFileName(file));
                Mp3FileReader reader = new(file);
                if ((output.Position == 0) && (reader.Id3v2Tag != null))
                {
                    output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    output.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
            output.Close();
        }

        public static WordInfo[] QueryWordInfos(string[] words, out bool haveError)
        {
            haveError = false;
            List<WordInfo> infos = new List<WordInfo>();
            for (int i = 0; i < words.Length; i++)
            {
                WordInfo? info = QueryWord(words[i]);
                if (info is not null)
                {
                    Console.WriteLine("查询单词 " + words[i]);
                    infos.Add(info);
                }
                else
                {
                    OutPut("查询单词 " + words[i] + "失败！\n", ConsoleColor.DarkRed);
                    haveError = true;
                }
            }
            return infos.ToArray();
        }

        public static void DownloadAllAudio(WordInfo[] words, string savePath)
        {
            foreach (var w in words)
            {
                string fileName = GetMp3Path(savePath, w.Word);// MP3路径
                string url = w.AudioUrl;// 下载链接
                                        // 下载MP3文件
                bool result = DownloadFile(url, fileName);
                if (!result)
                    OutPut("单词 " + w.Word + "的音频文件下载失败！", ConsoleColor.DarkRed);
                else
                    Console.WriteLine("下载单词 " + w.Word + "的音频");
                // Thread.Sleep(25);
            }
        }
        public static string GetMp3Path(string dir, string wordName)
        {
            return dir + wordName + ".mp3";
        }
        public static string GetDownloadDir()
        {
            return GetDurDir() + "下载文件夹\\";
        }

        public static string GetDurDir()
        {
            string? file = Process.GetCurrentProcess()?.MainModule?.FileName;
            if (file == null)
                throw new Exception("无法获取当前路径！");
            return Path.GetDirectoryName(file) + "\\";
        }
        public static WordInfo? QueryWord(string word)
        {
            string url = "https://cn.bing.com/dict/search?q=" + word;
         //   using HttpClient httpClient = new();
            string content = httpClient.GetStringAsync(url).Result;
            // 单词的的读音
            Regex pronRegex = new Regex("https://.*?\\.mp3");
            MatchCollection pronCollection = pronRegex.Matches(content);
            if (pronCollection.Count == 0)
                return null;
            return new WordInfo(word, pronCollection[0].Value);
        }

        // 接口
        //  static readonly string url = "https://fanyi.sogou.com/reventondc/synthesis?text=government&speed=1&lang=en&from=translateweb&speaker=1";
        public static string CombineUrl(string word)
        {
            return $"https://fanyi.sogou.com/reventondc/synthesis?text={HttpUtility.UrlEncode(word, System.Text.Encoding.UTF8)}&lang=en&speaker=6";
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        public static bool DownloadFile(string url, string fileName)
        {
            try
            {
               // using HttpClient httpClient = new();
                using FileStream fs = new(fileName, FileMode.Create);
                using HttpResponseMessage response = httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                //var total = response.Content.Headers.ContentLength;
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                byte[] buffer = new byte[4096];// 缓存
                int length;
                while ((length = responseStream.Read(buffer, 0, buffer.Length)) != 0)
                    fs.Write(buffer, 0, length);
                fs.Close();
                return true;
            }
            catch
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);
                return false;
            }
        }
        public static string[] ReadTxt(string file)
        {
            var lines = File.ReadAllLines(file).Where((line) => !string.IsNullOrEmpty(line));
            lines = lines.Where((line) => !StrCode.HaveChinese(line));
            return lines.ToArray();
        }

        public static void CreateLyrics(string[] mp3Files, string lrcFile)
        {
            if (File.Exists(lrcFile))
                File.Delete(lrcFile);
            using LrcFileHelper lrcFileHelper = new(lrcFile);
            foreach (var f in mp3Files)
            {
                // 计算歌词显示时长（毫秒）
                long lenMs = (long)(new Mp3File(f).Duration * 1000.0);
                // 每个文件名就是一个歌词
                string? lyric = Path.GetFileNameWithoutExtension(f);
                // 添加歌词
                lrcFileHelper.Append(lyric, lenMs);
                Console.WriteLine($"添加歌词：{lyric} 长度{lenMs / 1000.0:N2}秒");
            }
            lrcFileHelper.Close();
        }
        private static void SortAsFileCreatTime(ref string[] files)
        {
            Array.Sort(files, delegate (string x, string y) { return new FileInfo(x).CreationTime.CompareTo(new FileInfo(y).CreationTime); });
        }

        private static void OutPut(string text, ConsoleColor color)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = currentColor;
        }
    }
}