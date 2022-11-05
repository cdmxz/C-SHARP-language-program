using System;
using System.Drawing;
using System.Net;
using System.Text;
using System.Web;
using ZXing;
using ZXing.QrCode;

namespace 鹰眼OCR.QRCode
{
    class LocalQRCode
    {
        /// <summary>
        /// 本地生成二维码
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static Bitmap LocalGenerateQRCode(int size, string text)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            options.CharacterSet = "UTF-8"; // 内容编码
                                            // 二维码的宽高
            options.Width = size;
            options.Height = size;
            options.Margin = 1;             // 二维码的边距,单位不是固定像素
            writer.Options = options;
            return writer.Write(text);
        }

        /// <summary>
        /// 在线生成二维码
        /// </summary>
        /// <param name="size">大小</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static Bitmap OnlineGenerateQRCode(int size, string text)
        {
            string uri = "https://www.52mfei.cn/api/qrcode/?text=" + HttpUtility.UrlEncode(text, Encoding.UTF8) + "&size=" + size.ToString();
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            request.Timeout = 6000;
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                return (Bitmap)Image.FromStream(response.GetResponseStream());
            }
        }

        /// <summary>
        /// 本地识别二维码
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string LocalIdentifyQRCode(Image img)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";  // 内容编码
            Bitmap bitmap = (Bitmap)img;
            Result result = reader.Decode(bitmap);
            if (result == null)
                throw new Exception("未能识别二维码！");
            return string.Format($"二维码类型：{result.BarcodeFormat}{Environment.NewLine}二维码内容：{result.Text}");
        }
    }
}
