using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using 鹰眼OCR_WPFCore.Models;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class UpdateViewModel : ObservableObject
    {
        private readonly string Url = "https://villageestation.com/update/yyocr.json";

        [ObservableProperty]
        private bool _isUpdateAvailable;

        [ObservableProperty]
        private UpdateData _updateData;

        [ObservableProperty]
        private string _versionInfo;

        public UpdateViewModel()
        {
            VersionInfo = "当前版本：V" + System.Windows.Forms.Application.ProductVersion + "\r\n\r\n";
            CheckUpdate();

            // 写一个消息通知，mainviewmodel有更新
            //WeakReferenceMessenger.Default.Register<>(this, (data) =>
            //{
            //    UpdateData = data;
            //    IsUpdateAvailable = true;
            //});
        }

        private async void CheckUpdate()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(Url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                var updateInfo = JsonSerializer.Deserialize<UpdateData>(json);

                string ver = System.Windows.Forms.Application.ProductVersion;
                Version lastest = new Version(updateInfo.Latest.Version);
                Version current = new Version(ver);

                if (lastest > current)
                {
                    UpdateData = updateInfo;
                    IsUpdateAvailable = true;
                }
                VersionInfo += "更新版本：" + updateInfo.Latest.Version;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                UpdateData = 鹰眼OCR_WPFCore.Models.UpdateData.CreateTest();
            }
        }
    }
}
