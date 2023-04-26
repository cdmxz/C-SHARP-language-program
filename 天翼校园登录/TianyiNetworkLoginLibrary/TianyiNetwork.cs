using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using TianyiNetworkLoginLibrary.Config;
using TianyiNetworkLoginLibrary.Entity;
using TianyiNetworkLoginLibrary.Utils;


namespace TianyiNetworkLoginLibrary
{
    /// <summary>
    /// 登录tianyi校园宽带
    /// 本类没有实现portal认证
    /// </summary>
    public class TianyiNetwork : IDisposable
    {
        private readonly string password; // 登录密码
        private readonly RequestEntity requestEntity;
        private readonly HttpClient httpClient;

        /// <summary>
        /// 是否释放了资源
        /// </summary>
        public bool IsDisposed { get; private set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        public TianyiNetwork(string username, string password)
        {
            httpClient = new();
            requestEntity = new();
            // 获取重定向后的连接
            string redirectUrl = GetRedirectedUrl();
            string nasip, clientip;

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                // 解析redirectUrl里的nasip和userip，然后保存到文件
                GetNasIpAndUserIp(redirectUrl, out nasip, out clientip);
                ConfigFile.SaveNasIPAndUserIp(nasip, clientip);
            }
            else
            {
                // 如果获取不到重定向连接，就无法解析redirectUrl里的nasip和userip，
                // 此时使用上次保存到文件里的nasip和userip
                ConfigFile.GetNasIPAndUserIp(out nasip, out clientip);
            }
            requestEntity.NasIp = nasip;
            requestEntity.ClientIp = clientip;
            requestEntity.Mac = NetworkUtil.GetMacAddressByNetworkIp(clientip);
            requestEntity.Username = username;
            this.password = password;
        }

        ~TianyiNetwork()
        {
            Dispose();
        }

        /// <summary>
        /// 获取重定向后的连接
        /// </summary>
        /// <returns>返回重定向后的url，如果返回Empty，表示已连接到外网</returns>
        private string GetRedirectedUrl()
        {
            const string url = "http://www.msn.cn";
            // 获取不到则返回空值
            string redirectedUrl = string.Empty;
            using HttpResponseMessage response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var str = response.RequestMessage?.RequestUri?.ToString();
                if (str != null)
                {
                    if (new Uri(str).Host != new Uri(url).Host)
                        redirectedUrl = str;
                }
            }
            return redirectedUrl;
        }

        /// <summary>
        /// 从重定向连接里获取NasIp和UserIp
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="nasip"></param>
        /// <param name="clientIp"></param>
        private static void GetNasIpAndUserIp(string redirectUrl, out string nasip, out string clientIp)
        {
            // 获取重定向后的url的nasip和userip
            // http://enet.10000.gd.cn:10001/qs/main.jsp?wlanacip=61.146.140.73&wlanuserip=10.190.7.233
            nasip = Regex.Match(redirectUrl, @"(?<=acip=)([0-9\.]+)").Value;
            clientIp = Regex.Match(redirectUrl, @"(?<=userip=)([0-9\.]+)").Value;
            if (string.IsNullOrEmpty(clientIp))
                throw new Exception("无法获取：" + nameof(clientIp));
            if (string.IsNullOrEmpty(nasip))
                throw new Exception("无法获取：" + nameof(nasip));
        }

        /// <summary>
        ///  获取校验字符串
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetVerifyCodeString()
        {
            string url = "http://enet.10000.gd.cn:10001/client/vchallenge";
            // 设置请求参数
            requestEntity.SetNowUnixTimestamp();
            string md5Str = NetworkUtil.GetMd5(requestEntity.Version + requestEntity.ClientIp + requestEntity.NasIp + requestEntity.Mac + requestEntity.Timestamp + RequestEntity.Secret);
            requestEntity.SetAuthenticator(md5Str);
            string jsonText = requestEntity.GetJson();
            using var content = new StringContent(jsonText);
            // 发送请求
            using HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonObj= JsonNode.Parse(result)?.AsObject();
                // 返回 rescode = 0 表示请求成功
                if (jsonObj?["rescode"]?.ToString() == "0")
                {
                    var verifyCodeStr = jsonObj["challenge"]?.ToString();
                    if (verifyCodeStr != null)
                        return verifyCodeStr;
                }
            }
            throw new Exception("无法获取：verifyCodeStr");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool Login()
        {
            string url = "http://enet.10000.gd.cn:10001/client/login";
            // 设置请求参数
            string verifyCodeStr = GetVerifyCodeString();
            requestEntity.SetNowUnixTimestamp();
            string md5Str = NetworkUtil.GetMd5(requestEntity.ClientIp + requestEntity.NasIp + requestEntity.Mac + requestEntity.Timestamp + verifyCodeStr + RequestEntity.Secret);
            requestEntity.SetAuthenticator(md5Str);
            LoginRequestEntity loginRequestEntity = new(password, requestEntity);
            string jsonText = loginRequestEntity.GetJson();
            using StringContent content = new(jsonText);
            // 发送请求
            using HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonObj = JsonNode.Parse(result)?.AsObject();
                var rescode = jsonObj?["rescode"]?.ToString();
                if (rescode == null)
                    throw new Exception($"{nameof(rescode)}为空！");
                // 判断 rescode
                var msg = GetErrorMsg(rescode);
                if (string.IsNullOrEmpty(msg))
                    return true;
                else
                    throw new Exception(msg);
            }
            return false;
        }


        /// <summary>
        /// 是否连接到外网
        /// </summary>
        /// <returns></returns>
        public bool IsInternet()
        {
            var str = GetRedirectedUrl();
            return string.IsNullOrEmpty(str);
        }


        
        private static string? GetErrorMsg(string rescode)
        {
            return rescode switch
            {
                "-1" => "登录参数错误",
                "13016000" => ("账号错误"),
                "13012000" => ("密码错误"),
                "13017000" => ("暂停服务/账号欠费"),
                // rescode=0表示成功
                _ => null,
            };
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            string url = "http://enet.10000.gd.cn:10001/client/logout";
            // 设置参数
            requestEntity.SetNowUnixTimestamp();
            string md5Str = NetworkUtil.GetMd5(requestEntity.ClientIp + requestEntity.NasIp + requestEntity.Mac + requestEntity.Timestamp + RequestEntity.Secret);
            requestEntity.SetAuthenticator(md5Str);
            LogoutRequestEntity logoutRequestEntity = new(requestEntity);
            using var content = new StringContent(logoutRequestEntity.GetJson());
            // 发送请求
            using HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var jsonObj = JsonNode.Parse(result)?.AsObject();
                // 返回rescode=0表示注销成功
                return jsonObj?["rescode"]?.ToString() == "0";
            }
            return false;
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public void Dispose()
        {
            if (!IsDisposed)
            {
                IsDisposed = true;
                httpClient.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
