using System.Drawing;

namespace 鹰眼OCR_Extensions.OCR
{
    public class BaiduOCRParameter
    {
        public required string Mode { get; set; }
        public required Bitmap Image { get; set; }
        public required string DownloadedPath { get; set; }
        public required string LangType { get; set; }
        public required string IdCardSide { get; set; }
    }
}
