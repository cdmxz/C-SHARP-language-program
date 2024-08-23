using System.IO;

namespace 鹰眼OCR_Extensions.Audio
{
    public class WavInfo
    {
        /// <summary>
        /// 是否为wav文件
        /// </summary>
        public bool IsWavFile { get; set; }
        /// <summary>
        /// 声道数
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// 比率
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        /// 音频文件长度
        /// </summary>
        public int Len { get; set; }

        public WavInfo(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception("文件不存在！");
            }

            using FileStream fs = new FileStream(fileName, FileMode.Open);
            using BinaryReader br = new BinaryReader(fs);
            br.ReadBytes(8);
            // 9-12个四个字节（wave标志）
            string flag = new string(br.ReadChars(4));
            IsWavFile = flag.ToLower() == "wave";
            br.ReadBytes(10);
            // 23-24两个字节（声道数）
            Channel = br.ReadInt16();
            // 25-28四个字节（采样率）
            Rate = br.ReadInt32();
            br.ReadBytes(6);
            int bit = br.ReadInt16();// 采样位数
            Len = GetWavLen(fs.Length / 1024.0, Channel, Rate, bit);
        }

        // 获取wav文件长度
        private int GetWavLen(double size, int channel, int rate, int bit)
        {
            double len = size * 8.0 / (rate / 1000.0 * bit * channel);
            return (int)(len + 0.5);// 四舍五入
        }
    }
}