using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 腾讯会议摸鱼助手.Record;
using 腾讯会议摸鱼助手.Utils;

namespace 腾讯会议摸鱼助手
{
    public partial class FrmQueryKeywords : Form
    {
        private RecordMic? record;
        private bool flag;

        public FrmQueryKeywords()
        {
            InitializeComponent();
            flag = false;
        }

        private void button_Record_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                Stop();
            }
            else
            {
                Start();
            }
            flag = !flag;
            button_Record.Text = flag ? "停止" : "录音";
        }

        private void Start()
        {
            string file = Util.GetTempWavFilePath();
            string? dir = Path.GetDirectoryName(file);
            if (dir != null && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (record != null && !record.IsDisposed)
                record.Dispose();
            record = new RecordMic(file);
            record.StartRecording();
        }

        private void Stop()
        {

        }
    }
}
