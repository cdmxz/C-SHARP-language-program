using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using 翻译神器.TranslationApi;
using 翻译神器.WinApi;

namespace 翻译神器
{
    public delegate void RefreshFrmTranslateInfoDelegate(FrmTranslateInfo frmTranslateInfo);

    public partial class FrmTranslate : Form
    {
        // **************************************翻译窗口**************************************
        /// <summary>
        /// 刷新FrmTranslateInfo委托
        /// </summary>
        public RefreshFrmTranslateInfoDelegate? RefreshFrmTranslateInfoDelegate;

        /// <summary>
        /// 翻译窗口
        /// </summary>
        /// <param name="translateApi"></param>
        /// <param name="frmTranslateInfo"></param>
        /// <param name="windowName"></param>
        /// <param name="windowClass"></param>
        public FrmTranslate(ITranslateApi translateApi, FrmTranslateInfo frmTranslateInfo, string windowName, string windowClass)
        {
            InitializeComponent();
            //  comboBox1_SourceLang.SelectedIndex = FrmMain.AutoPressKey;
            if (frmTranslateInfo.DestLang is not null)
                comboBox2_DestLang.SelectedItem = frmTranslateInfo.DestLang;
            checkBox1_AutoSend.Checked = frmTranslateInfo.AutoSend;
            this.translateApi = translateApi;
            comboBox1_SourceLang.SelectedIndex = 0;
            comboBox2_DestLang.SelectedIndex = 0;
            textBox2_Dest.ReadOnly = true;
            this.windowName = windowName;
            this.windowClass = windowClass;
        }

        private readonly ITranslateApi translateApi;
        private readonly string[] Youdao_to = { "en", "ru", "ja" };  // 目标语言名称
        private readonly string[] Baidu_to = { "en", "ru", "jp" };   // 目标语言名称
        private readonly string[] Tengxun_to = { "en", "ru", "ja" }; // 目标语言名称
        private readonly Keys[] Key = { Keys.T, Keys.Y };
        private readonly string windowName;
        private readonly string windowClass;

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                string dst = Translate();
                this.WindowState = FormWindowState.Minimized;
                SendText(dst);  // 将译文发送到窗口
                this.Close();
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            bool visible = !string.IsNullOrEmpty(textBox1_Source.Text);
            label3_Source.Visible = visible;
            label4_Dest.Visible = visible;
        }


        #region 右键菜单
        private void textBox1_撤销ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.Undo();

        private void textBox1_剪切ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.Cut();

        private void textBox1_复制ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.Copy();

        private void textBox1_粘贴ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.Paste();

        private void textBox1_删除ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.SelectedText = "";

        private void textBox1_全选ToolStripMenuItem_Click(object sender, EventArgs e) => textBox1_Source.SelectAll();

        private void textBox1_从右到左的顺序RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1_Source.RightToLeft == RightToLeft.No)
            {
                textBox1_Source.RightToLeft = RightToLeft.Yes;
                textBox1_从右到左的顺序RToolStripMenuItem.Checked = true;
            }
            else
            {
                textBox1_Source.RightToLeft = RightToLeft.No;
                textBox1_从右到左的顺序RToolStripMenuItem.Checked = false;
            }
        }

        private void textBox2_撤销toolStripMenuItem1_Click(object sender, EventArgs e) => textBox2_Dest.Undo();

        private void textBox2_剪切toolStripMenuItem2_Click(object sender, EventArgs e) => textBox2_Dest.Cut();

        private void textBox2_复制toolStripMenuItem3_Click(object sender, EventArgs e) => textBox2_Dest.Copy();

        private void textBox2_粘贴toolStripMenuItem4_Click(object sender, EventArgs e) => textBox2_Dest.Paste();

        private void textBox2_删除toolStripMenuItem5_Click(object sender, EventArgs e) => textBox2_Dest.SelectedText = "";

        private void textBox2_全选toolStripMenuItem6_Click(object sender, EventArgs e) => textBox2_Dest.SelectAll();

        private void textBox2_从右到左的顺序toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (textBox2_Dest.RightToLeft == RightToLeft.No)
            {
                textBox2_Dest.RightToLeft = RightToLeft.Yes;
                toolStripMenuItem7.Checked = true;
            }
            else
            {
                textBox2_Dest.RightToLeft = RightToLeft.No;
                toolStripMenuItem7.Checked = false;
            }
        }
        #endregion


        private void button_Translate_Click(object sender, EventArgs e)
        {
            textBox2_Dest.Text = Translate();
        }

        // 翻译
        private string Translate()
        {
            string from = "auto";
            string to = GetDestLangCode();
            string dst = "";
            try
            {
                if (string.IsNullOrEmpty(textBox1_Source.Text))
                    return string.Empty;
                // 调用翻译接口
                if (comboBox2_DestLang.SelectedItem.ToString() != "不翻译")
                {
                    translateApi.TextTranslate(textBox1_Source.Text, from, to, out string src, out dst);
                }
                else
                {
                    dst = textBox1_Source.Text;
                }
                // 保存当前选项
                RefreshFrmTranslateInfo();
                return dst;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "翻译", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dst;
        }

        /// <summary>
        /// 刷新FrmTranslateInfo
        /// </summary>
        private void RefreshFrmTranslateInfo()
        {
            var info = new FrmTranslateInfo()
            {
                AutoSend = checkBox1_AutoSend.Checked,
                DestLang = comboBox2_DestLang.SelectedItem.ToString(),
                Key = this.Key[comboBox1_SourceLang.SelectedIndex],
            };
            this.RefreshFrmTranslateInfoDelegate?.Invoke(info);
        }

        /// <summary>
        /// 获取目标语言代码
        /// </summary>
        /// <returns></returns>
        private string GetDestLangCode()
        {
            if (translateApi.ApiChineseName.Contains("百度"))
                return Baidu_to[comboBox2_DestLang.SelectedIndex];
            else if (translateApi.ApiChineseName.Contains("有道"))
                return Youdao_to[comboBox2_DestLang.SelectedIndex];
            else
                return Tengxun_to[comboBox2_DestLang.SelectedIndex];
        }

        // 向gta窗口发送文本
        private void SendText(string txt)
        {
            // 查找窗口句柄
            IntPtr hwnd = Api.FindWindowHandle(windowName, windowClass);
            // 激活窗口
            Api.SetForegroundWindowAndWait(hwnd, Api.WAIT_MILLISECONDS);
            Api.SendKey(Key[comboBox1_SourceLang.SelectedIndex]);
            Thread.Sleep(500);
            SendKeys.SendWait(txt);
            Thread.Sleep(1000);
            if (checkBox1_AutoSend.Checked) // 自动发送（按下回车键）
                Api.SendKey(Keys.Enter);
        }
    }
}
