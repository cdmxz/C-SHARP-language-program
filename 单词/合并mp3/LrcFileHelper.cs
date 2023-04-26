namespace 合并mp3
{
    /// <summary>
    /// 歌词生成类
    /// </summary>
    public class LrcFileHelper : IDisposable
    {
        private readonly StreamWriter _writer;
        private bool _isDisposed = false;
        long totalMilliSec;


        public LrcFileHelper(string fileName)
        {
            _writer = new StreamWriter(fileName, false);
            //initLrcFile("合成的单词");
            totalMilliSec = 0L;
        }


        private void initLrcFile(string title)
        {
            AppendLine("[ti:" + title + "]");
            AppendLine("[offset:0]");
        }


        public void Append(string lyric, long lenMs)
        {
            string time = ConvertTime(totalMilliSec);
            totalMilliSec += lenMs;
            AppendLine(time + lyric);
        }


        private static string ConvertTime(long milli)
        {
            //3600000 milliseconds in an hour
            long hr = milli / 3600000;
            milli -= 3600000 * hr;
            //60000 milliseconds in a minute
            long min = milli / 60000;
            milli -= (60000 * min);
            //1000 milliseconds in a second
            long sec = milli / 1000;
            double ms = milli - 1000 * sec;
            ms /= 1000;
            string minStr = min < 10 ? "0" + min : min + "";
            string secStr = sec < 10 ? "0" + sec : sec + "";
            string msStr = ms < 10.0 ? "0" + ms.ToString("N2") : ms.ToString("N2") + "";
            return ("[" + minStr + ":" + secStr + msStr.TrimStart('0') + "]");
        }

        private void AppendLine(String line)
        {
            _writer.WriteLine(line);
        }

        public void Close() => Dispose();

        public void Dispose()
        {
            if (!_isDisposed)
                _writer.Close();
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
