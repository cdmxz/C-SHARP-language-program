using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 鹰眼OCR_WPFCore.Models
{
    /// <summary>
    /// 软件更新 实体类
    /// </summary>
    public partial class UpdateData : ObservableObject
    {
        public static UpdateData CreateTest()
        {
            return new UpdateData
            {
                Latest = new LatestItem
                {
                    Version = "V3.0.0",
                    Date = DateTime.Now,
                    Change = "修复bug"
                },
                Downloads = new List<DownloadsItem>
                {
                    new DownloadsItem
                    {
                        Name = "",
                        Url = "" 
                    }
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        [ObservableProperty]
        private LatestItem _latest;


        /// <summary>
        /// 
        /// </summary>

        [ObservableProperty]
        private List<DownloadsItem> _downloads;

        public class LatestItem
        {
            /// <summary>
            /// 
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public DateTime Date { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string Change { get; set; }

            public override string ToString()
            {
                return $"版本：{Version}\r\n更新时间：{Date}\r\n更新说明：{Change}";
            }
        }

        public class DownloadsItem
        {
            /// <summary>
            /// 下载点
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 下载链接
            /// </summary>
            public string Url { get; set; }
        }



    }
}
