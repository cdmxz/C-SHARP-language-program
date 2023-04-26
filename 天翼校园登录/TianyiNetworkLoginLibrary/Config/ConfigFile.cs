using System.Text.Json;
using TianyiNetworkLoginLibrary.Encryption;

namespace TianyiNetworkLoginLibrary.Config
{
    /// <summary>
    /// 配置文件类
    /// </summary>
    public static class ConfigFile
    {
        private static string ConfigFilePath;
        private static DataEntity? dataEntity;
        private static readonly AesEncryption aes;

        static ConfigFile()
        {
            // 设置aes加密密钥和iv，用于加密账号密码保存到本地
            aes = new("aqwertyuioplkjhg", "mnbvcxzxbnmg4568");
            string? path = Path.GetDirectoryName(Environment.ProcessPath) ?? throw new Exception("无法获取配置文件路径");
            ConfigFilePath = Path.Combine(path, "TianyiNetworkLogin.json");
            ReadConfigFile();
        }

        /// <summary>
        /// 设置配置文件路径
        /// </summary>
        /// <param name="configFilePath"></param>
        public static void SetConfigFilePath(string configFilePath)
        {
            ConfigFilePath = configFilePath;
            ReadConfigFile();
        }

        /// <summary>
        /// 从文件读取json数据，然后反序列化
        /// </summary>
        private static void ReadConfigFile()
        {
            if (File.Exists(ConfigFilePath))
            {
                string json = File.ReadAllText(ConfigFilePath);
                dataEntity = JsonSerializer.Deserialize<DataEntity>(json);
            }
        }


        /// <summary>
        /// 保存nasip和userip到文件
        /// </summary>
        /// <param name="nasip"></param>
        /// <param name="userip"></param>
        public static void SaveNasIPAndUserIp(string nasip, string userip)
        {
            dataEntity ??= new DataEntity();
            dataEntity.Nasip = nasip;
            dataEntity.Userip = userip;
            string json = JsonSerializer.Serialize(dataEntity);
            File.WriteAllText(ConfigFilePath, json);
        }


        /// <summary>
        /// 从文件获取nasip和userip
        /// </summary>
        /// <param name="nasip"></param>
        /// <param name="userip"></param>
        public static void GetNasIPAndUserIp(out string nasip, out string userip)
        {
            if (dataEntity != null)
            {
                nasip = dataEntity.Nasip;
                userip = dataEntity.Userip;
            }
            else
            {
                nasip = string.Empty;
                userip = string.Empty;
            }
        }

        /// <summary>
        /// 获取账号和密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool GetUsernameAndPwd(out string username, out string pwd)
        {
            if (dataEntity != null)
            {
                username = dataEntity.UserName;
                pwd = dataEntity.Password;
                username = aes.DecryptStringFromBase64(username);
                pwd = aes.DecryptStringFromBase64(pwd);
                return true;
            }
            else
            {
                username = string.Empty;
                pwd = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 设置账号和密码
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        public static void SaveUsernameAndPwd(string username, string pwd)
        {
            dataEntity ??= new DataEntity();
            //dataEntity.UserName = username;
            //dataEntity.Password = pwd;
            dataEntity.UserName = aes.EncryptStringToBase64(username);
            dataEntity.Password = aes.EncryptStringToBase64(pwd);
            string json = JsonSerializer.Serialize(dataEntity);
            File.WriteAllText(ConfigFilePath, json);
        }
    }
}
