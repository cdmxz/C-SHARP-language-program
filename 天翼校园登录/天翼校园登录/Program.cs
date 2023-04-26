using TianyiNetworkLoginLibrary;
using TianyiNetworkLoginLibrary.Config;

namespace 天翼校园登录
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //string user = "15347548743";
                //string pwd = "13642594156a";
                string user;
                string pwd;
                if (ConfigFile.GetUsernameAndPwd(out string username, out string password))
                {
                    user = username;
                    pwd = password;
                }
                else
                {
                    Console.Write("请输入账号：");
                    user = Console.ReadLine() ?? "";
                    Console.Write("请输入密码：");
                    pwd = Console.ReadLine() ?? "";
                }
                using TianyiNetwork tianyi = new(user, pwd);
                if(args.Length > 0)
                {
                    tianyi.Logout();
                    return;
                }
                if (tianyi.IsInternet())
                {
                    Console.WriteLine("已连接到外网");
                }
                else if (tianyi.Login())
                {
                    Console.WriteLine("登录成功");
                }
                // 保存账号密码
                ConfigFile.SaveUsernameAndPwd(user, pwd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}