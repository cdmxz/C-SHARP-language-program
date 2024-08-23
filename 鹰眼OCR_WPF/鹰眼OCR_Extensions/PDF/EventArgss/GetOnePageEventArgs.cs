using System.Drawing;

namespace 鹰眼OCR_Extensions.PDF.EventArgss
{
    public class GotOnePageEventArgs : EventArgs
    {
        public Bitmap Image { get; set; }
        public int Current { get; set; }
        public int Total { get; set; }

        public GotOnePageEventArgs(Bitmap img, int current, int total)
        {
            Image = img;
            Current = current;
            Total = total;
        }
    }
}
