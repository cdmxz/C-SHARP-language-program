using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace 鹰眼OCR_Common.Messages.MessageParam
{
    /// <summary>
    /// 截图消息参数
    /// </summary>
    public class ScreenshotParam
    {
        /// <summary>
        /// 截图完成后的图像
        /// </summary>
        [NotNull]
        public required Bitmap CaptureImage { get; set; }

        /// <summary>
        /// 选择的截图区域，坐标为屏幕坐标
        /// </summary>
        public Rectangle CaptureRectangle { get; set; }


        /// <summary>
        /// 目标窗口句柄
        /// Start方法传入了才会返回 
        /// </summary>
        public IntPtr DestHandle { get; set; }

    }
}
