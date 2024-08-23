using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;

namespace 鹰眼OCR_Extensions.QRCode
{
    public class QRCode
    {
        /// <summary>
        /// 本地生成二维码
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static Bitmap GenerateQRCode(int size, string text)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
            };
            QrCodeEncodingOptions options = new()
            {
                DisableECI = true,
                CharacterSet = "UTF-8", // 内容编码
                                        // 二维码的宽高
                Width = size,
                Height = size,
                Margin = 1             // 二维码的边距,单位不是固定像素
            };
            writer.Options = options;
            return writer.Write(text);
        }

        /// <summary>
        /// 本地识别二维码
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string IdentifyQRCode(Image img)
        {
            // create a barcode reader instance
            var reader = new BarcodeReader();
            // load a bitmap
            var barcodeBitmap = (Bitmap)img;
            // detect and decode the barcode inside the bitmap
            var result = reader.Decode(barcodeBitmap);
            // do something with the result
            if (result == null)
            {
                throw new Exception("未能识别二维码！");
            }

            return string.Format($"二维码类型：{result.BarcodeFormat}{Environment.NewLine}二维码内容：{result.Text}");
        }
    }
}
