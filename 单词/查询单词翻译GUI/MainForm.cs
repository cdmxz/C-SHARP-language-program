using QueryWordLib.StrHelper;
using QueryWordLib.WordHelper;
using QueryWordLib.WordHelper.Api;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace 查询单词翻译GUI
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource cts;

        public MainForm()
        {
            InitializeComponent();
            richTextBox1.AllowDrop = true;
            richTextBox1.DragEnter += RichTextBox1_DragEnter;
            richTextBox1.DragDrop += RichTextBox1_DragDrop;
        }

        private void RichTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string path = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                OpenAndQuery(path);
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message);
            }

        }

        private void RichTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All; //表明是所有类型的数据，如文件路径
        }

        private void ToolStripButton_Open_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
                return;
            string path = openFileDialog1.FileName;
            try
            {
                OpenAndQuery(path);
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message);
            }
        }

        private void OpenAndQuery(string path)
        {
            string outFile = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "_译文.docx";
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(path);
            saveFileDialog1.FileName = outFile;
            QueryWord(path);
        }


        private void QueryWord(string inFile)
        {
            cts = new CancellationTokenSource();
            var token = cts.Token;
            Thread thread = new Thread(() =>
            {
                StreamReader sr = new StreamReader(inFile, System.Text.Encoding.UTF8);
                string line;
                while (!cts.IsCancellationRequested && (line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;
                    if (StrCode.HaveChinese(line))
                    { // 如果一行单词里面含有中文则表示该行单词已经含有翻译，则不翻译直接输出
                        AppendToRichtextbox(WordInfo.Parse(line));
                    }
                    else
                    {
                        var result = YoudaoWord.QueryExplanation(line);
                        AppendToRichtextbox(result);
                    }
                }
                sr.Close();
                this.Invoke(new Action(() => SaveToFile(saveFileDialog1.FileName)));
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }


        /// <summary>
        /// 将单词信息添加到Richtextbox
        /// </summary>
        /// <param name="info"></param>
        private void AppendToRichtextbox(WordInfo info)
        {
            this.Invoke(new Action(() =>
            {
                int start = GetRichTextboxTextLength();
                // 添加文字到Richtextbox
                AppendToRichtextbox(info.Word + "    ");
                // 查找并选中刚刚添加的文字
                FindRichtextbox(info.Word, start);
                // 设置选中的文字的字体为16磅
                richTextBox1.SelectionFont = new System.Drawing.Font("微软雅黑", 16f);
                start = GetRichTextboxTextLength();
                AppendToRichtextbox(info.Explanation + "\r\n");
                FindRichtextbox(info.Explanation, 0);
                richTextBox1.SelectionFont = new System.Drawing.Font("微软雅黑", 10.5f);
                start = GetRichTextboxTextLength();
                AppendToRichtextbox(info.Phonetic + "\r\n");
                if (!string.IsNullOrEmpty(info.Phonetic))
                    AppendToRichtextbox("\r\n");
                FindRichtextbox(info.Phonetic, 0);
                richTextBox1.SelectionFont = new System.Drawing.Font("微软雅黑", 10.5f);
            }));
        }

        private int GetRichTextboxTextLength()
        {
            return this.richTextBox1.Text.Length;
        }

        private int AppendToRichtextbox(string text)
        {
            this.richTextBox1.AppendText(text);
            return this.richTextBox1.Text.Length;
        }

        private void FindRichtextbox(string text, int start)
        {
            int end = this.richTextBox1.Text.Length;
            richTextBox1.Find(text, start, end, RichTextBoxFinds.MatchCase);
        }

        private void ToolStripButton_Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SaveToFile(saveFileDialog1.FileName);
                }
                catch (Exception ex)
                {
                    ShowLog("保存失败！原因：" + ex.Message);
                }
            }

        }

        private void SaveToFile(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower().TrimStart('.');// 获取文件的扩展名
            switch (ext)
            {
                case "docx":
                    RichTextBoxHelper.SaveToWordDocument(richTextBox1, fileName);
                    break;
                case "txt":
                    File.WriteAllText(fileName, richTextBox1.Text);
                    break;
            }
        }


        private void ShowLog(string log)
        {
            this.Invoke(new Action(() => this.toolStripStatusLabel_Log.Text = log));
        }

        private void ToolStripButton_Cancel_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }
    }
}
