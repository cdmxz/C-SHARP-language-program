using Screenshot_WPF.Api;
using System.Drawing;
using System.Windows;



namespace Screenshot_WPF.Helper
{
    /// <summary>
    /// 截图助手类
    /// </summary>
    public class ScreenShotHelper
    {
        ///// <summary>
        ///// 拷贝整个屏幕并返回 BitmapSource
        ///// </summary>
        ///// <returns></returns>
        //public static BitmapSource CopyScreenToBitmapSource()
        //{
        //    double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
        //    double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

        //    System.Windows.Size bmpSize = new((int)screenWidth, (int)screenHeight);
        //    Bitmap bitmap = CopyScreen(0, 0, bmpSize.Width, bmpSize.Height);
        //    IntPtr hBitmap = IntPtr.Zero;
        //    try
        //    {
        //        hBitmap = bitmap.GetHbitmap();
        //        BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
        //            hBitmap,
        //            IntPtr.Zero,
        //            System.Windows.Int32Rect.Empty,
        //            BitmapSizeOptions.FromEmptyOptions());
        //        return bitmapSource;
        //    }
        //    finally
        //    {
        //        if (hBitmap != IntPtr.Zero)
        //        {
        //            WinApi.DeleteObject(hBitmap);
        //        }
        //        bitmap.Dispose();
        //    }
        //}

        /// <summary>
        /// 拷贝部分屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap CopyScreen(Rect rect)
        {
           return CopyScreen((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
        }

        /// <summary>
        /// 拷贝部分屏幕
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static Bitmap CopyScreen(int x, int y, int w, int h)
        {
            Bitmap bmp = new(w, h);
            //  bmp.SetResolution(120, 120); // 设置DPI为120
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(w, h));
            }

            return bmp;
        }

        /// <summary>
        /// 根据窗口句柄拷贝整个窗口
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CopyWindowByHandle(nint handle)
        {
            Rect rect = WinApi.GetWindowRect(handle);
            return CopyScreen(rect);
        }
    }
}
