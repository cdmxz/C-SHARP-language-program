using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using System.IO;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_Extensions.OCR;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Messages;
using 鹰眼OCR_WPFCore.Models;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class OptionViewModel : ObservableObject
    {
        public OptionViewModel(ConfigData viewData)
        {
            _viewData = viewData.OptionViewData;
        }

        [ObservableProperty]
        private OptionViewData _viewData;

        [RelayCommand]
        private void Execute(string para)
        {
            switch (para)
            {
                case "TestOCR":
                    TestOCR();
                    break;
                case "TestTranslate":
                    TestTranslate();
                    break;
                case "OpenLocal":
                    OpenLocal();
                    break;
                case "DeleteLocal":
                    DeleteLocal();
                    break;
                case "ConfirmDeleteLocal":
                    ConfirmDeleteLocal();
                    break;
            }
        }



        private void TestOCR()
        {
            try
            {
                Baidu.OCRKeyTestAsync(ViewData.ApiKey, ViewData.SecretKey);
                SendBaiduKeyUpdateMessgae();
                NotifierHelper.ShowSuccess("测试成功");
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
        }

        private void TestTranslate()
        {
            try
            {
                Baidu.TranslateKeyTestAsync(ViewData.AppId, ViewData.Password);
                SendBaiduKeyUpdateMessgae();
                NotifierHelper.ShowSuccess("测试成功");
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
        }


        private void SendBaiduKeyUpdateMessgae()
        {
            // 键值对 MessageTokens.OptionViewModel是瞎写的，只要不重复就行
            WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.OptionViewModel, "BaiduKeyUpdate" } }), MessageTokens.HomeViewModel);
        }

        private void OpenLocal()
        {
            try
            {
                Process pro = new();
                pro.StartInfo.FileName = "explorer.exe";
                pro.StartInfo.Arguments = ConfigData.ConfigDir;
                pro.StartInfo.UseShellExecute = true;
                pro.Start();
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
        }

        private void DeleteLocal()
        {
            NotifierHelper.ShowWarning("鼠标右键点击'删除按钮'才能删除");
        }

        private void ConfirmDeleteLocal()
        {
            DelDir(ConfigData.OCRDir);
            DelDir(ConfigData.TTSDir);
            DelDir(ConfigData.FormDir);
            DelDir(ConfigData.AsrDir);
            DelDir(ConfigData.PhotographDir);
            NotifierHelper.ShowSuccess("删除成功");
        }

        private void DelDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
        }
    }
}
