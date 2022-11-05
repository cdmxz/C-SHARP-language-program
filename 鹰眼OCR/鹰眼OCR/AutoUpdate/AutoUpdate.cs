using System.Diagnostics;
using System.IO;
using Update_DirectDownload;

namespace 鹰眼OCR
{
    public class AutoUpdate
    {
        private readonly static string xmlUrl;
        private readonly static string programPath;
        private readonly static string replaceFilePath;
        private readonly static Update update;
        public static event Update.RefreshProgressEventHandler RefreshProgressEvent
        {
            add => update.RefreshProgressEvent += value;
            remove => update.RefreshProgressEvent -= value;
        }

        static AutoUpdate()
        {
            xmlUrl = "https://gitee.com/fuhohua/DownloadUpdate/raw/master/yingyanocr.xml";
            programPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            replaceFilePath = programPath + "\\替换文件.exe";
            if (File.Exists(replaceFilePath))
                File.Delete(replaceFilePath);
            update = new Update(xmlUrl, programPath, replaceFilePath);
        }


        public static bool CheckUpdate() => update.CheckUpdate();

        public static string GetUpdateInfo() => update.Info;

        public static void DownloadAndReplaceFile()
        {
            update.DownloadUpdate();
            File.WriteAllBytes(replaceFilePath, Properties.Resources.ReplaceFile);
            update.ReplaceFile(Process.GetCurrentProcess().MainModule.FileName);
        }
    }
}
