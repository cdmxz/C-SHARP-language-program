using OpenCvSharp;


namespace 鹰眼OCR
{
    public partial class FrmPhotograph : Form
    {
        public FrmPhotograph()
        {
            InitializeComponent();
        }


        // 拍照识别委托
        public delegate void PhotographEventHandler(Image img);
        public event PhotographEventHandler PhotographEvent;

        /// <summary>
        /// 窗体显示的坐标
        /// </summary>
        public System.Drawing.Point Position { get; set; }

        private VideoCapture capture = new VideoCapture(0);
        private bool flag = true;

        // 拍照
        private void button3_Photograph_Click(object sender, EventArgs e)
        {
            Image img;
            do
            {
                img = (Image)pictureBox1.Image?.Clone();
            }
            while (img == null);
           
            OnPhotograph(img);
        }

        private void open()
        {
            //此处参考网上的读取方法
            int sleepTime = (int)Math.Round(1000 / capture.Fps);
            // 声明实例 Mat类
            Mat image = new Mat();

            // 进入读取视频每镇的循环
            while (flag)
            {
                capture.Read(image);
                //判断是否还有没有视频图像 
                if (image.Empty())
                    break;
                // 在picturebox中播放视频， 需要先转换成bitmap格式
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }

                MemoryStream stream = image.ToMemoryStream();
                pictureBox1.Image = Image.FromStream(stream);
                Cv2.WaitKey(sleepTime);
            }
            capture.Dispose();
        }

        private void OnPhotograph(Image img) => PhotographEvent?.Invoke(img);

        // 窗口加载时，自动连接摄像头
        private void FrmPhotograph_Load(object sender, EventArgs e)
        {
            this.Location = Position;
        }


        private void FrmPhotograph_Shown(object sender, EventArgs e)
        {
            open();
        }

        private void FrmPhotograph_FormClosed(object sender, FormClosedEventArgs e)
        {
            flag = false;
        }
    }
}
