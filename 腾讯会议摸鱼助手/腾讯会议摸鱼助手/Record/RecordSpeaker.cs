using NAudio.Wave;

namespace 腾讯会议摸鱼助手.Record
{
    /// <summary>
    /// 使用Naudio录制扬声器的声音
    /// </summary>
    public class RecordSpeaker : IDisposable
    {
        public bool IsDisposed;
        public WasapiLoopbackCapture capture;
        public WaveFileWriter writer;
        public event EventHandler? RecordingStopped;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="outFile">输出文件路径</param>
        public RecordSpeaker(string outFile)
        {
            capture = new WasapiLoopbackCapture();
            capture.WaveFormat = new WaveFormat(16000, 16, 1);
            writer = new WaveFileWriter(outFile, capture.WaveFormat);
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRecording()
        {
            if (IsDisposed)
                return;
            capture.DataAvailable += (_, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
            };
            // 录音停止事件
            capture.RecordingStopped += OnRecordingStoppedEvent;
            capture.StartRecording();
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRecording()
        {
            capture.StopRecording();
        }

        /// <summary>
        /// 引发录音停止事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnRecordingStoppedEvent(object? sender, EventArgs e)
        {
            RecordingStopped?.Invoke(this, e);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
            capture.Dispose();
            writer.Dispose();
            GC.SuppressFinalize(this);
        }

        ~RecordSpeaker()
        {
            Dispose();
        }

        /// <summary>
        /// 录制sec秒
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="outFile"></param>
        public static void RecordSpeakerBySeconds(double sec, string outFile)
        {
            using RecordSpeaker record = new(outFile);
            record.StartRecording();
            int milliSec = (int)(sec * 1000);
            Thread.Sleep(milliSec);
            record.StopRecording();
        }
    }
}
