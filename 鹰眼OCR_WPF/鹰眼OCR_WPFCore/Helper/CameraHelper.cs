using System.Management;

namespace 鹰眼OCR_WPFCore.Helper
{
    public class CameraHelper
    {
        /// <summary>
        /// 获取电脑摄像头列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetCameras()
        {
            var cameraNames = new List<string>();
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE (PNPClass = 'Image' OR PNPClass = 'Camera')"))
            {
                foreach (var device in searcher.Get())
                {
                    var name = device["Caption"].ToString();
                    if (name != null)
                    {
                        cameraNames.Add(name);
                    }
                }
            }

            return cameraNames.ToArray();
        }
    }
}
