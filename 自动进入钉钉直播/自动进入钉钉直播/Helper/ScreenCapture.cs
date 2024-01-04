using System.Drawing.Drawing2D;

namespace 自动进入钉钉直播.Helper
{
    class ScreenCapture
    {
        /// <summary>
        /// 从指定坐标截取指定大小区域
        /// </summary>
        public static Image Capture(Rectangle rect)
        {
            Bitmap bit = new(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bit))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;// 质量设为最高
                g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
            }
            return bit;
        }
    }
}
