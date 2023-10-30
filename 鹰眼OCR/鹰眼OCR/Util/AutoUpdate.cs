using System.Diagnostics;
using System.IO;
using Update_DirectDownload;
using Common;

namespace 鹰眼OCR.Util
{
    public class AutoUpdate
    {
        private readonly static string xmlUrl;
        private readonly static string programPath;
        private readonly static string replaceFilePath;
        private readonly static SoftwareUpdate softwareUpdate;
        /// <summary>
        /// 刷新进度委托
        /// </summary>
        public static ProgressDelegate ProgressDelegate { get; set; }

        static AutoUpdate()
        {
            xmlUrl = "https://gitee.com/fuhohua/DownloadUpdate/raw/master/yingyanocr.xml";
            programPath = Path.GetDirectoryName(Environment.ProcessPath);
            replaceFilePath = programPath + "\\替换文件.exe";
            if (File.Exists(replaceFilePath))
                File.Delete(replaceFilePath);
            softwareUpdate = new SoftwareUpdate(xmlUrl, programPath, replaceFilePath);
        }


        public static bool CheckUpdate() => softwareUpdate.CheckUpdate();

        public static string GetUpdateInfo() => softwareUpdate.Info;

        public static void DownloadAndReplaceFile()
        {
            softwareUpdate.DownloadUpdate(ProgressDelegate);
            File.WriteAllBytes(replaceFilePath, Properties.Resources.ReplaceFile);
            softwareUpdate.ReplaceFile(Environment.ProcessPath);
        }
    }
}
