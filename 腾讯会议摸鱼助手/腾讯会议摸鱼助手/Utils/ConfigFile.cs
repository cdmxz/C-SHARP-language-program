using Newtonsoft.Json;

namespace 腾讯会议摸鱼助手.Utils
{
    internal class ConfigFile
    {
        /// <summary>
        ///  配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get;
        } = Path.GetDirectoryName(Environment.ProcessPath) + "\\腾讯会议摸鱼助手.json";

        /// <summary>
        /// 文件是否存在
        /// </summary>
        public static bool Exist
        {
            get => File.Exists(ConfigPath);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="data"></param>
        public static void Write(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllText(ConfigPath, json);
        }

        /// <summary>
        /// 读文件，并且将内容序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T? Read<T>()
        {
            if (!Exist)
                return default;
            string json = File.ReadAllText(ConfigPath);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
