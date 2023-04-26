namespace TianyiNetworkLoginLibrary.Config
{
    /// <summary>
    /// 保存到配置文件的数据的实体类
    /// </summary>
    internal class DataEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Nasip { get; set; }
        public string Userip { get; set; }
        public DataEntity(string username, string password, string nasip, string userip)
        {
            UserName = username;
            Password = password;
            Nasip = nasip;
            Userip = userip;
        }

        public DataEntity()
        {
            UserName = string.Empty;
            Password = string.Empty;
            Nasip = string.Empty;
            Userip = string.Empty;
        }
    }
}
