using NAudio.Wave;
using System.Diagnostics.CodeAnalysis;

namespace 鹰眼OCR_Extensions.Audio
{
    public class PlayAudio : IDisposable
    {
        // 播放结束
        public delegate void PlayStoppedEventHandler(string audioName, string? mode);
        public event PlayStoppedEventHandler? PlayStoppedEvent;

        private readonly WaveOut waveOut;
        [NotNull]
        private string fileName;
        private string? mode;
        private bool IsDisposed;

        public PlayAudio()
        {
            waveOut = new WaveOut();
            waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
        }

        /// <summary>
        /// 异步播放音频文件
        /// </summary>
        /// <param name="fileName"></param>
        public void PlayAsync(string fileName, string? mode = null)
        {
            this.fileName = fileName;
            this.mode = mode;
            if (IsPlaying())
            {
                CancelPlay();
            }

            Task.Run(Play);
        }

        /// <summary>
        /// 取消播放
        /// </summary>
        public void CancelPlay()
        {
            waveOut.Stop();
        }

        // 播放停止
        private void OnPlayStopped(string fileName, string? mode)
        {
            PlayStoppedEvent?.Invoke(fileName, mode);
        }

        /// <summary>
        /// 播放状态
        /// 正在播放返回true
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying()
        {
            return waveOut?.PlaybackState == PlaybackState.Playing;
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        private void Play()
        {
            try
            {
                using AudioFileReader audioFileReader = new AudioFileReader(fileName);
                waveOut.Init(audioFileReader);
                waveOut.Play();
                // 如果没有下面这个循环，则播放1s后自动停止
                while (waveOut.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(200);
                }
            }
            catch (Exception)
            {
                waveOut?.Stop();
                throw;
            }
        }

        ~PlayAudio()
        {
            Dispose();
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                waveOut?.Dispose();
            }
        }

        // 播放停止事件
        private void WaveOut_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            OnPlayStopped(fileName, mode);
        }
    }
}
