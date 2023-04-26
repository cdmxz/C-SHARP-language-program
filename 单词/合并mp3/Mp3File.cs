using NAudio.Wave;

namespace 合并mp3
{
    public class Mp3File
    {
        /// <summary>  
        /// Mp3文件地址  
        /// </summary>  
        /// <param name="fileName"></param>  
        public Mp3File(string fileName)
        {
            using Mp3FileReader mp3FileReader = new(fileName);
            Duration = mp3FileReader.TotalTime.TotalSeconds;
        }

        /// <summary>  
        /// 时长  
        /// </summary>  
        public double Duration
        {
            get; private set;
        }

    }
}
