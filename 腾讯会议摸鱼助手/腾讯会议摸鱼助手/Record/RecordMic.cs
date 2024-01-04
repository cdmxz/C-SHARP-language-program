using NAudio.Wave;

namespace 腾讯会议摸鱼助手.Record
{
    internal class RecordMic : IDisposable
    {
        public bool IsDisposed;
        public WaveInEvent mic;
        public WaveFileWriter writer;
        public event EventHandler? RecordingStopped;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="outFile">输出文件路径</param>
        public RecordMic(string outFile)
        {
            mic = new WaveInEvent();
            mic.WaveFormat = new WaveFormat(16000, 16, 1);
            writer = new WaveFileWriter(outFile, mic.WaveFormat);
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void StartRecording()
        {
            // 避免释放资源后出现 对象的引用未设置到对象的实例
            if (IsDisposed)
                return;
            mic.DataAvailable += Mic_DataAvailable;
            // 录音停止事件
            mic.RecordingStopped += OnRecordingStoppedEvent;
            mic.StartRecording();
        }

        private void Mic_DataAvailable(object? sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void StopRecording()
        {
            mic?.StopRecording();
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
            mic.Dispose();
            writer.Dispose();
            GC.SuppressFinalize(this);
        }

        ~RecordMic()
        {
            Dispose();
        }

        /// <summary>
        /// 录制sec秒
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="outFile"></param>
        public static void RecordMicBySeconds(double sec, string outFile)
        {
            using RecordMic record = new(outFile);
            record.StartRecording();
            int milliSec = (int)(sec * 1000);
            Thread.Sleep(milliSec);
            record.StopRecording();
        }
    }
}
