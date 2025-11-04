//using OpenCvSharp;
//using Sdcb.PaddleInference;
//using Sdcb.PaddleOCR;
//using Sdcb.PaddleOCR.Models;
//using Sdcb.PaddleOCR.Models.Local;
//using System.Drawing;

//namespace 鹰眼OCR_Extensions.OCR
//{
//    public class Paddle : IDisposable
//    {
//        private PaddleOcrAll all;
//        public bool IsDisposed { get; set; }

//        public Paddle()
//        {
//            FullOcrModel model = LocalFullModels.ChineseV4;
//            all = new PaddleOcrAll(model, PaddleDevice.Gpu(deviceId:1))
//            {
//                AllowRotateDetection = true, /* 允许识别有角度的文字 */
//                Enable180Classification = false, /* 允许识别旋转角度大于90度的文字 */
//            };
//        }

//        public async Task<string> GeneralBasicAsync(Image img, string langType = "")
//        {
//            string result = "";
//            await Task.Run(() =>
//            {
//                // img转Mat
//                using Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat((Bitmap)img);
//                if (src.Channels() == 4)
//                {
//                    Cv2.CvtColor(src, src, ColorConversionCodes.BGRA2BGR);
//                }
//                PaddleOcrResult res = all.Run(src);
//                src.Dispose();
//                result = res.Text;
//            });
//            return result;
//            //Console.WriteLine("Detected all texts: \n" + result.Text);
//            //foreach (PaddleOcrResultRegion region in result.Regions)
//            //{
//            //    Console.WriteLine($"Text: {region.Text}, Score: {region.Score}, RectCenter: {region.Rect.Center}, RectSize:    {region.Rect.Size}, Angle: {region.Rect.Angle}");
//            //}
//        }

//        public void Dispose()
//        {
//            if (IsDisposed)
//                return;
//            all?.Dispose();
//            IsDisposed = true;
//        }

//    }
//}
