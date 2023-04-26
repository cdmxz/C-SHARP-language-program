using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace TianyiNetworkLoginLibrary.Utils
{
    /// <summary>
    /// 网络工具类
    /// </summary>
    internal class NetworkUtil
    {
        /// <summary>
        /// 获取网卡ip与参数ip相同的网卡 的mac地址
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetMacAddressByNetworkIp(string ip)
        {
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var Interface in networkInterfaces)
            {
                foreach (var item in Interface.GetIPProperties().UnicastAddresses)
                {
                    if (item.Address.ToString() == ip)
                    {
                        return BitConverter.ToString(Interface.GetPhysicalAddress().GetAddressBytes());
                    }
                }
            }
            return string.Empty;
        }

        public static string GetMd5(string str)
        {
            MD5 md5 = MD5.Create();
            StringBuilder sb = new();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            foreach (byte b in data)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

    }
}
