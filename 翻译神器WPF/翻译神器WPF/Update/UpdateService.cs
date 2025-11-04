using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;

namespace 翻译神器WPF.Update
{
    public class UpdateService
    {
        private static readonly string UpdatedFileUrl = "https://gitee.com/fuhohua/DownloadUpdate/raw/master/fanyishenqi.json";


        public Version CurrentVersion { get; private set; }

        public UpdateInfo? Info { get; private set; }

        public bool IsNewVersionAvailable { get; private set; } = false;

        public UpdateService()
        {
            try
            {
                CurrentVersion = GetCurrentVersion() ?? new Version(0, 0, 0);
            }
            catch
            {
            }
        }

        public async Task<bool> CheckAsync()
        {
            using var http = new HttpClient();

            //  获取更新信息

            //using var fileStream = File.OpenRead("F:\\C#\\C#源代码\\翻译神器WPF\\fanyishenqi.json");
            //Info = await JsonSerializer.DeserializeAsync<UpdateInfo>(fileStream);

            using var jsonStream = await http.GetStreamAsync(UpdatedFileUrl);
            Info = await JsonSerializer.DeserializeAsync<UpdateInfo>(jsonStream);

            if (Info is null)
            {
                return false;
            }

            // 比较版本
            var current = CurrentVersion;
            if (!Version.TryParse(Info.Version, out var remote))
            {
                throw new Exception($"无法解析服务器版本号：{Info.Version}");
            }
            IsNewVersionAvailable = (remote > current);
            return IsNewVersionAvailable;
        }

        private static Version? GetCurrentVersion()
        {
            Version? curVersion = Assembly.GetExecutingAssembly().GetName().Version;
            return curVersion;
        }

        /// <summary>
        /// 打开更新链接
        /// </summary>
        public void OpenUpdateUrl()
        {
            if (string.IsNullOrEmpty(Info?.Url))
            {
                throw new InvalidOperationException("更新链接为空");
            }
            Process pro = new();
            pro.StartInfo.FileName = Info.Url;
            pro.StartInfo.UseShellExecute = true;
            pro.Start();
        }

    }
}