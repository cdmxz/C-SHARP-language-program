using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using 鹰眼OCR.Audio;

namespace 鹰眼OCR
{
    // 语音识别委托
    public delegate void SpeechRecognitionHandler();

    public partial class FrmAsr : Form
    {
        public SpeechRecognitionHandler SpeechRecognition { get; set; }

        /// <summary>
        /// 窗体显示的坐标
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// 保存到本地的音频文件名称 
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SaveDir { get; set; }

        /// <summary>
        /// 录制的语言类型（普通话或英文）
        /// </summary>
        public string RecordLang { get; set; }

        /// <summary>
        /// wav文件采样率（16000或8000），默认16000
        /// </summary>
        public int SamplingRate { get; set; }

        private string[] _SpeechLang;
        public string[] SpeechLang
        {
            get { return _SpeechLang; }
            set
            {
                _SpeechLang = value;
                RefreshLanguage(_SpeechLang);
            }
        }

        /// <summary>
        /// 最大录制时间
        /// </summary>
        public int MaxSec { get; set; }

        private Recorder recorder = new Recorder();
        private PlayAudio playAudio = new PlayAudio();
        private int maxTime;
        public FrmAsr()
        {
            InitializeComponent();
            SamplingRate = 16000;
        }

        // 开始录音
        private void button1_Start_Click(object sender, EventArgs e) => Start();

        private void Start()
        {
            FileName = SaveDir + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".wav";
            if (!Directory.Exists(Path.GetDirectoryName(FileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(FileName));
            try
            {
                if (recorder.Starting)
                    throw new Exception("正在录音！");
                if (File.Exists(FileName))
                    File.Delete(FileName);
                recorder.Start(FileName, SamplingRate);
                maxTime = MaxSec;
                timer1_RecordingTime.Enabled = true;
                label2.Text = MaxSec.ToString() + "秒";
                label3.ForeColor = Color.DeepSkyBlue;
                label3.Text = "当前状态：正在录音";
            }
            catch (Exception ex)
            {
                if (!Setting_Other.SaveRecord)
                {
                    if (Directory.Exists(Path.GetDirectoryName(FileName)))
                        Directory.Delete(Path.GetDirectoryName(FileName));
                }
                label3.Text = ex.Message;
            }
        }

        // 结束录音
        private void button2_Stop_Click(object sender, EventArgs e) => Stop();

        private void Stop()
        {
            try
            {
                if (!recorder.Starting)
                    return;
                recorder.Stop();
                timer1_RecordingTime.Enabled = false;
                label3.ForeColor = Color.Black;
                label3.Text = "当前状态：已停止录音";
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }

        // 播放录音
        private void button3_Play_Click(object sender, EventArgs e)
        {
            try
            {
                if (recorder.Starting)
                    throw new Exception("请先结束录音");

                // 如果当前未播放，则播放
                if (!playAudio.IsPlaying())
                {
                    if (!File.Exists(FileName))
                        throw new Exception("文件不存在");
                    playAudio.PlayAsync(FileName);
                }
                else
                    playAudio.CancelPlay();
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }

        // 识别录音
        private void button_Recognition_Click(object sender, EventArgs e) => Recognition();

        private void Recognition()
        {
            if (recorder.Starting)
                Stop();
            SpeechRecognition?.Invoke();// 调用委托，识别录制的语音
        }

        private void FrmSoundRecording_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && !recorder.Starting)    // 按下的是N键 并且 已停止录音
                Start();
            else if (e.KeyCode == Keys.N && recorder.Starting)// 按下的是N键 并且 正在录音
                Recognition();
        }

        private void FrmSoundRecording_FormClosing(object sender, FormClosingEventArgs e) => Stop();

        // 显示窗口后自动开始录音
        private void FrmSoundRecording_Shown(object sender, EventArgs e) => Start();

        private void FrmSoundRecording_Load(object sender, EventArgs e) => this.Location = Position;

        private void timer1_RecordingTime_Tick(object sender, EventArgs e)
        {   // 显示录制时间
            try
            {
                maxTime--;
                label2.Text = maxTime.ToString() + "秒";
                if (maxTime <= 0)
                {
                    Recognition();
                    throw new Exception("已到达最大录制时间。");
                }
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }

        private void comboBox_Lang_SelectedIndexChanged(object sender, EventArgs e) => RecordLang = comboBox_Lang.SelectedItem.ToString();

        public void RefreshLanguage(string[] lang)
        {
            if (lang == null)
                return;
            try
            {
                comboBox_Lang.Items.Clear();
                comboBox_Lang.Items.AddRange(lang);
                comboBox_Lang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                label3.Text = ex.Message;
            }
        }
    }
}
