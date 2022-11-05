using AForge.Video.DirectShow;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace 鹰眼OCR
{
    public partial class FrmPhotograph : Form
    {
        public FrmPhotograph()
        {
            InitializeComponent();
            comboBox2_SwitchResolution.Items.Clear();
        }

        // 拍照识别委托
        public delegate void PhotographEventHandler(Image img);
        public event PhotographEventHandler PhotographEvent;

        /// <summary>
        /// 窗体显示的坐标
        /// </summary>
        public Point Position { get; set; }

        private FilterInfoCollection videoDevices;
        private string errMessage = null;   // 错误消息
        private int currentDeviceIndex = -1;// 当前选中的摄像头设备索引 (等于comboBox1_SwitchingDevice.SelectedIndex)


        // 扫描视频设备
        private void button1_ScanDevices_Click(object sender, EventArgs e)
        {
            try
            {
                // 查找视频输入设备并添加到comboBox1
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                    throw new Exception("没有摄像头");
                int curDeviceCount = comboBox1_SwitchingDevice.Items.Count;
                comboBox1_SwitchingDevice.Items.Clear();
                foreach (FilterInfo device in videoDevices)
                    comboBox1_SwitchingDevice.Items.Add(device.Name);
                int newDeviceCount = comboBox1_SwitchingDevice.Items.Count;
                groupBox1.Text = (curDeviceCount > newDeviceCount) ? "发现新摄像头" : "未发现新摄像头";
            }
            catch (Exception ex)
            {
                comboBox1_SwitchingDevice.Items.Add(ex.Message);
                errMessage = ex.Message;     // 保存错误消息
                groupBox1.Text = ex.Message; // 在groupBox1左上角显示
                videoDevices = null;
            }
            finally
            {
                comboBox1_SwitchingDevice.SelectedIndex = 0;
            }
        }


        // 连接视频设备
        private void button2_ConnectingDevice_Click(object sender, EventArgs e) => ConnectingDevice(false);

        /// <summary>
        /// 连接摄像头
        /// </summary>
        /// <param name="isSwitchCameras">是否切换分辨率</param>
        private void ConnectingDevice(bool isSwitchCameras)
        {
            if (comboBox1_SwitchingDevice.SelectedItem.ToString() == errMessage)
            {   // 如果没有视频设备
                groupBox1.Text = errMessage;
                return;
            }
            // 避免重复按下“连接”按钮
            if (!isSwitchCameras)
            {
                if (currentDeviceIndex == comboBox1_SwitchingDevice.SelectedIndex)
                {
                    groupBox1.Text = "当前设备已连接";
                    return;
                }
            }

            // 如果是当前正在切换分辨率
            // 如果正在运行，则停止
            if (videoPlayer1.IsRunning)
            {
                videoPlayer1.SignalToStop();
                videoPlayer1.WaitForStop();
            }
            currentDeviceIndex = comboBox1_SwitchingDevice.SelectedIndex; // 保留连接的视频设备索引
            VideoCaptureDevice videoCapture = new VideoCaptureDevice(videoDevices[comboBox1_SwitchingDevice.SelectedIndex].MonikerString);
            if (comboBox2_SwitchResolution.Items.Count == 0)
            {
                foreach (var item in videoCapture.VideoCapabilities)
                    comboBox2_SwitchResolution.Items.Add($"{item.FrameSize.Width}x{item.FrameSize.Height}");
                comboBox2_SwitchResolution.SelectedIndex = 0;
            }
            videoCapture.VideoResolution = videoCapture.VideoCapabilities[comboBox2_SwitchResolution.SelectedIndex];
            videoPlayer1.VideoSource = videoCapture;
            videoPlayer1.Start();
            groupBox1.Text = "正在拍照";
        }

        // 拍照
        private void button3_Photograph_Click(object sender, EventArgs e)
        {
            if (comboBox1_SwitchingDevice.SelectedItem.ToString() == errMessage)
            {   // 如果没有视频设备
                groupBox1.Text = errMessage;
                return;
            }

            if (!videoPlayer1.IsRunning)// 如果未运行
            {
                groupBox1.Text = "未运行";
                return;
            }

            // 将图片保存到本地
            groupBox1.Text = "已拍照";
            OnPhotograph(videoPlayer1.GetCurrentVideoFrame());
        }

        private void OnPhotograph(Image img) => PhotographEvent?.Invoke(img);

        // 窗口加载时，自动连接摄像头
        private void FrmPhotograph_Load(object sender, EventArgs e)
        {
            this.Location = Position;
            button1_ScanDevices_Click(null, null);// 先扫描视频设备，再连接摄像头
            ConnectingDevice(false);//连接摄像头
        }


        // 关闭摄像头并清理资源
        private void FrmPhotograph_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoPlayer1 != null)
            {
                if (videoPlayer1.IsRunning)
                {
                    videoPlayer1.SignalToStop();
                    videoPlayer1.WaitForStop();
                }
                videoPlayer1.Dispose();
            }
        }

        // 切换分辨率时关闭并重新打开摄像头
        private void comboBox2_SwitchResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 重新以选择的分辨率连接摄像头
            ConnectingDevice(true);
        }
    }
}
