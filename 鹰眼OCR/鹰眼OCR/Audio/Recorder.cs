﻿using NAudio.Wave;
using System;
using System.IO;

namespace 鹰眼OCR.Audio
{
    class Recorder
    {
        private WaveIn waveIn = new WaveIn();
        private WaveFileWriter waveFile = null;

        /// <summary>
        /// 获取录制状态（是否启动）
        /// </summary>
        public bool Starting { get => _Starting; }
        private bool _Starting;

        ///// <summary>
        /////  获取录音时长
        ///// </summary>
        //public double RecordedTime
        //{
        //    get
        //    {
        //        if (waveFile == null)
        //            return -1;
        //        return (double)waveFile.Length / waveFile.WaveFormat.AverageBytesPerSecond;
        //    }
        //}

        public Recorder()
        {
            // 录音中接收到数据事件
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
        }

        /// <summary>
        /// 开始录音
        /// </summary>
        public void Start(string fileName, int rate)
        {
            try
            {
                // 设置录音格式
                waveIn.WaveFormat = new WaveFormat(rate, 16, 1);
                DisposeObject(ref waveFile);
                waveFile = new WaveFileWriter(fileName, waveIn.WaveFormat);
                // 开始录音
                waveIn.StartRecording();
                _Starting = true;
            }
            catch
            {
                Stop();
                DisposeObject(ref waveFile);
                if (File.Exists(fileName))
                    File.Delete(fileName);
                throw new Exception("请插入麦克风！");
            }
        }

        /// <summary>
        /// 停止录音
        /// </summary>
        public void Stop()
        {
            _Starting = false;
            waveIn.StopRecording();
            DisposeObject(ref waveFile);
        }

        private void DisposeObject<T>(ref T obj)
        {
            if (obj == null)
                return;
            if (obj is IDisposable disposable)
            {
                disposable.Dispose();
                obj = default;
            }
        }

        /// <summary>
        /// 开始录音回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveIn_DataAvailable(object sender, WaveInEventArgs e) => waveFile.Write(e.Buffer, 0, e.BytesRecorded);

        ~Recorder()
        {
            Dispose();
        }

        private void Dispose()
        {
            DisposeObject(ref waveIn);
            DisposeObject(ref waveFile);
            GC.SuppressFinalize(this);
        }
    }
}
