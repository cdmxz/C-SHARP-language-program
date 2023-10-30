using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using 鹰眼OCR.QRCode;

namespace 鹰眼OCR
{
    public partial class FrmQrCode : Form
    {
        private string _Content;
        public string Content
        {
            set { _Content = value; }
        }

        public Point Position { get; set; }

        public FrmQrCode()
        {
            InitializeComponent();
            label_ShowErr.Visible = false;
        }

        private void ShowQrCode(int size, string text)
        {
            try
            {
                ShowError(false, null);
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = CreateQrCode(size, text);
            }
            catch (Exception ex)
            {
                ShowError(true, ex.Message);
            }
        }

        private Bitmap CreateQrCode(int size, string text) => QRCode.QRCode.GenerateQRCode(size, text);

        private void FrmQRCode_Load(object sender, EventArgs e)
        {
            this.Location = Position;
            ShowQrCode(trackBar1.Value, _Content);
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                ShowQrCode(trackBar1.Value, _Content);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;
            try
            {
                using (var fileDialog = new SaveFileDialog())
                {
                    fileDialog.Filter = "图片（*.png、*.bmp、*.jpg、*.gif）|*.png;*.bmp;*.jpg;*.gif";
                    fileDialog.FileName = "*.png";
                    fileDialog.AddExtension = true;
                    fileDialog.DefaultExt = "png";
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ImageFormat format = ImageFormat.Png;
                        string ext = Path.GetExtension(fileDialog.FileName);
                        if (ext == ".bmp")
                            format = ImageFormat.Bmp;
                        else if (ext == ".jpg")
                            format = ImageFormat.Jpeg;
                        else if (ext == ".gif")
                            format = ImageFormat.Gif;
                        using (var img = CreateQrCode(trackBar1.Value, _Content))
                            img.Save(fileDialog.FileName, format);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(true, ex.Message);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label_ShowSize.Text = trackBar1.Value.ToString() + "PX";
            //  QrCode(trackBar1.Value, _Content);
        }

        private void ShowError(bool show, string err)
        {
            if (show)
            {
                pictureBox1.Visible = false;
                label_ShowErr.Dock = DockStyle.Fill;
                label_ShowErr.Visible = true;
                label_ShowErr.Text = err;
            }
            else
            {
                label_ShowErr.Dock = DockStyle.None;
                label_ShowErr.Visible = false;
                pictureBox1.Visible = true;
            }
        }
    }
}
