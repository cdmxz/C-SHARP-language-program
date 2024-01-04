using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 腾讯会议摸鱼助手.Record
{
    internal class RecordHelper
    {
        /// <summary>
        /// 录制milliSec秒 扬声器
        /// </summary>
        /// <param name="outFile"></param>
        public static void RecordBySeconds(int milliSec, string outFile, CancellationToken ct)
        {
            using RecordSpeaker record = new(outFile);
            //using RecordMic record = new(outFile);
            record.StartRecording();
            // 循环等待
            ThreadSleep(milliSec, ct);
            record.StopRecording();
        }

        /// <summary>
        /// 录制milliSec秒 麦克风
        /// </summary>
        /// <param name="outFile"></param>
        public static void RecordMicBySeconds(int milliSec, string outFile, CancellationToken ct)
        {
            using RecordMic record = new(outFile);
            record.StartRecording();
            // 循环等待
            ThreadSleep(milliSec, ct);
            record.StopRecording();
        }


        /// <summary>
        /// 使当前线程休眠
        /// 可取消
        /// </summary>
        /// <param name="milliseconds"></param>
        /// <param name="ct"></param>

        public static void ThreadSleep(int milliseconds, CancellationToken ct)
        {
            int ms = 50;
            int total = 0;
            if (milliseconds < ms)
            {
                Thread.Sleep(milliseconds);
                return;
            }

            while ((total + ms) <= milliseconds && !ct.IsCancellationRequested)
            {
                Thread.Sleep(ms);
                total += ms;
            }
        }

    }
}
