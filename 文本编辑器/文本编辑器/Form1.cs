using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


public struct ConfigFile
{
    //配置文件路径
    public string iniDirectory;//配置文件目录
    public string iniPath;//配置文件路径
    public string section;//ini文件中的节名       
    //ini文件中的项名
    public string key1;//自动换行
    public string key2;//状态栏
    public string key3;//右上角显示高和宽
    public string key4;//文件拖放
    public string key5;//不记住上次关闭时的大小
    public string key6;//不记住上次关闭时的位置
    public string key7;//窗体的高
    public string key8;//窗体的宽
    public string key9;//横坐标
    public string key10;//纵坐标
    public StringBuilder wordWrap;//是否自动换行
    public StringBuilder statusBar;//是否显示状态栏
    public StringBuilder heightAndWidth;//是否显示高和宽
    public StringBuilder Fd;//此项为true时，当打开的文件是rtf、doc、docx格式时，支持文本、图片和其它数据的拖放
    public StringBuilder RestoreFormSize;//启动时是否恢复上次关闭窗体前的大小
    public StringBuilder RestoreFormPosition;//启动时是否恢复上次关闭窗体前的位置
    public StringBuilder height;//高
    public StringBuilder width;//宽
    public StringBuilder x;//x坐标
    public StringBuilder y;//y坐标
    public int MAX;
    public string err;
};

namespace 文本编辑器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //定时器
            //启用定时器
            timer1.Enabled = true;
            //定时器的时间间隔设置为1000ms
            timer1.Interval = 1000;
            时间toolStripStatusLabel1.Text = "当前时间：" + DateTime.Now.ToString();

            //文本控件
            richTextBox1.AllowDrop = true;
            //取消richTextBox的字数限制
            richTextBox1.MaxLength = 0;
            //开启richTextBox自动换行
            richTextBox1.WordWrap = true;
            //避免中英文字体不统一
            richTextBox1.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            //自动换行
            richTextBox1.Multiline = true;
            //始终显示竖直滚动条
            richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            richTextBox1.HideSelection = false;

            //打开文件
            //设置文件类型
            openFileDialog1.Filter = filter;
            //设置默认文件名
            openFileDialog1.FileName = "";
            //设置默认文件类型显示顺序
            openFileDialog1.FilterIndex = 1;
            //是否记忆上次打开的目录
            openFileDialog1.RestoreDirectory = true;


            //保存文件
            //设置文件类型
            saveFileDialog1.Filter = filter;
            //设置默认文件名
            saveFileDialog1.FileName = "*.txt";
            //如果用户省略扩展名，是否自动在文件名中添加
            saveFileDialog1.AddExtension = true;
            //设置默认文件类型显示顺序
            saveFileDialog1.FilterIndex = 1;
            //是否记忆上次打开的目录
            saveFileDialog1.RestoreDirectory = true;


            int num;
            //获取系统字体
            if ((systemFont = FileOperation.GetSystemFont()) != null)
            {
                //将系统字体插入列表
                toolStripComboBox2.Items.AddRange(systemFont);
                if ((num = Array.LastIndexOf(systemFont, fontName)) != -1)
                    toolStripComboBox2.SelectedIndex = num;
                else
                    toolStripComboBox2.SelectedIndex = 0;
            }

            //将字号插入列表
            toolStripComboBox1.Items.AddRange(fontSize);
            if ((num = Array.LastIndexOf(fontSize, pound.ToString())) != -1)
                toolStripComboBox1.SelectedIndex = num;//设置字体大小
            else
                toolStripComboBox1.SelectedIndex = 0;

            //获得当前字体字号所在数组元素的下标
            j = Array.IndexOf(fontSize, richTextBox1.Font.Name.ToString());


            占位控件toolStripStatusLabel4.Text = "";
            //显示当前窗体宽和高
            label1.Text = string.Format(this.Width + "x" + this.Height);
        }

        private string filter = "文本(*.txt、*.ini、*.uni、*.xml、*.htm、*.html、*.config)|*.txt;*.ini;*.uni;*.xml;*.htm;*.html;*.config|文档(*.doc、*.docx、*.rtf)|*.doc;*.docx;*.rtf|所有文件(*.*)|*.*";
        private string ext;//文件扩展名
        private string error;//错误提示
        private string fileName;//打开或保存的文件名
        private string[] systemFont;//系统所有字体
        private string[] fontSize = { "初号", "小初", "一号", "小一", "二号", "小二", "三号", "小三", "四号", "小四", "五号", "小五", "六号", "小六", "七号", "八号", "5", "5.5", "6.5", "7.5", "8", "9", "10", "10.5", "11", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
        private float pound = 12f;//默认字号
        private string fontName = "微软雅黑";//默认字体名称
        private bool openFile = false;//是否打开了文件
        private bool saveFile = true;//判断用户上次编辑后是否保存
        private bool fileDrag = false;
        private int changeNumber = 1;//richTextBox修改次数
        private Font f = null;
        private ConfigFile cf = new ConfigFile();


        //初始化结构体
        private void InitializeStruct()
        {
            cf.iniDirectory = Environment.GetEnvironmentVariable("APPDATA") + @"\文本编辑器";
            cf.iniPath = Environment.GetEnvironmentVariable("APPDATA") + @"\文本编辑器\config.ini";
            cf.section = ("文本编辑器");
            cf.key1 = ("自动换行");
            cf.key2 = ("状态栏");
            cf.key3 = ("右上角显示高和宽");
            cf.key4 = ("文件拖放");
            cf.key5 = ("不记住上次关闭时的大小");
            cf.key6 = ("不记住上次关闭时的位置");
            cf.key7 = ("高");
            cf.key8 = ("宽");
            cf.key9 = ("横坐标");
            cf.key10 = ("纵坐标");
            cf.wordWrap = new StringBuilder(cf.MAX);
            cf.statusBar = new StringBuilder(cf.MAX);
            cf.heightAndWidth = new StringBuilder(cf.MAX);
            cf.Fd = new StringBuilder(cf.MAX);
            cf.RestoreFormSize = new StringBuilder(cf.MAX);
            cf.RestoreFormPosition = new StringBuilder(cf.MAX);
            cf.height = new StringBuilder(cf.MAX);
            cf.width = new StringBuilder(cf.MAX);
            cf.x = new StringBuilder(cf.MAX);
            cf.y = new StringBuilder(cf.MAX);
            cf.MAX = 10;
            cf.err = "Error";
        }


        //打开关于窗口
        private void 关于AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }


        //为各个控件、字段传值
        private void PassValue(string OPENFILE, string SAVEFILE, string TEXT, string LABLETEXT)
        {
            try
            {
                if (!string.IsNullOrEmpty(OPENFILE))
                    openFile = Convert.ToBoolean(OPENFILE);

                if (!string.IsNullOrEmpty(SAVEFILE))
                    saveFile = Convert.ToBoolean(SAVEFILE);

                if (!string.IsNullOrEmpty(TEXT))//判断不为null
                    this.Text = TEXT;

                if (!string.IsNullOrEmpty(LABLETEXT))
                    文本编码toolStripStatusLabel5.Text = LABLETEXT;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //当打开的文件是rtf、doc、docx格式时，支持文本、图片和其它数据的拖放
        private bool OpenRichAutoDragDrop(bool val, string ext)
        {
            if (val && (ext == ".doc" || ext == ".docx" || ext == ".rtf"))
            {
                richTextBox1.AllowDrop = false;
                //richTextBox1.DragEnter -= new DragEventHandler(richTextBox1_DragEnter);
                //richTextBox1.DragDrop -= new DragEventHandler(richTextBox1_DragDrop);
                richTextBox1.EnableAutoDragDrop = true;
                return true;
            }
            else
            {
                richTextBox1.AllowDrop = true;
                //richTextBox1.DragEnter += new DragEventHandler(richTextBox1_DragEnter);
                //richTextBox1.DragDrop += new DragEventHandler(richTextBox1_DragDrop);
                richTextBox1.EnableAutoDragDrop = false;
                return false;
            }
        }

        //新建文件
        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            if (!saveFile)//提示是否保存
            {
                DialogResult result = MessageBox.Show("是否保存更改？", "文本编辑器", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                    保存SToolStripButton_Click(null, null);
                else if (result == DialogResult.No)
                    richTextBox1.Clear(); //清空所有文本
                else
                    return;
            }

            richTextBox1.Clear();
            PassValue("false", "true", "无标题", "UTF-8");
        }


        //打开文件
        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!saveFile)//判断是否保存文件
                {
                    DialogResult result = MessageBox.Show("是否保存更改？", "文本编辑器", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                        保存SToolStripButton_Click(null, null);
                    else if (result == DialogResult.Cancel)
                        return;
                }

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog1.FileName;
                    string code = FileOperation.GetEncoding(fileName);//获得文件编码
                    Open(fileName, code);//以不同格或不同编码打开文件并显示到richtextbox

                    PassValue("true", "true", fileName, code);
                    richTextBox1.Modified = false;

                    //删除左上角文件名修改后添加的*号
                    if (this.Text.Substring(this.Text.Length - 2) == " *" && saveFile == false)
                    {
                        this.Text = this.Text.Remove(this.Text.Length - 2);
                        saveFile = true;
                    }
                }
            }
            catch (Exception ex)
            {
                error = string.Format($"打开{fileName}错误！\n原因：{ex.Message}");
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //勾选对应的文件编码复选框（位置：格式-以...编码打开）
        private void CheckFileCodeCheckBox(string code)
        {
            //先把所有复选框取消打勾
            openASCIIToolStripMenuItem.Checked = false;
            openUTF8ToolStripMenuItem.Checked = false;
            open带BOM的UTF8ToolStripMenuItem.Checked = false;
            openUTF16LEToolStripMenuItem.Checked = false;
            openUTF16BEToolStripMenuItem.Checked = false;
            openNet默认ToolStripMenuItem.Checked = false;

            switch (code)
            {
                case "ANSI":
                    openASCIIToolStripMenuItem.Checked = true;
                    break;
                case "UTF-8":
                    openUTF8ToolStripMenuItem.Checked = true;
                    break;
                case "带BOM的UTF-8":
                    open带BOM的UTF8ToolStripMenuItem.Checked = true;
                    break;
                case "UTF-16 LE":
                    openUTF16LEToolStripMenuItem.Checked = true;
                    break;
                case "UTF-16 BE":
                    openUTF16BEToolStripMenuItem.Checked = true;
                    break;
                default:
                    openNet默认ToolStripMenuItem.Checked = true;
                    break;
            }
        }


        /// <summary>
        /// 打开文件并将其内容显示到richtextbox
        /// </summary>
        /// <param name="filename">文件路径</param>
        /// <param name="code">文件编码格式</param>
        private void Open(string filename, string code)
        {
            CheckFileCodeCheckBox(code);//勾选对应的文件编码复选框
            ext = Path.GetExtension(filename).ToLower();//获取文件的扩展名

            if (ext == ".rtf")
            {//打开rtf文件
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.RichText);
                return;
            }
            else if (ext == ".doc" || ext == ".docx")
            {//打开文档文件
                OpenWord(filename);
                return;
            }
            else if (ext == ".uni")
            {
                richTextBox1.LoadFile(filename, RichTextBoxStreamType.UnicodePlainText);
                return;
            }

            //用与文件对应的编码打开文件
            StreamReader stream = new StreamReader(filename, CodeNameToCodeStream(code));
            richTextBox1.Text = stream.ReadToEnd();
            stream.Close(); //释放资源
        }


        //保存文件
        private void 保存SToolStripButton_Click(object sender, EventArgs e)
        {
            if (openFile)//判断是否打开了文件
            {
                SaveFile(fileName, FileOperation.GetEncoding(fileName));//保存文件
            }
            else
            {   //如果没有打开文件就显示另存为对话框
                另存为AToolStripMenuItem_Click(null, null);
            }

            //删除左上角文件名修改后添加的*号
            if (this.Text.Substring(this.Text.Length - 2) == " *" && saveFile == false)
            {
                this.Text = this.Text.Remove(this.Text.Length - 2);
                saveFile = true;
            }
            richTextBox1.Modified = false;
        }

        //文件另存为
        private void 另存为AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                SaveFile(fileName, "UTF-8");//保存文件，默认以"UTF-8"保存

                fileName = saveFileDialog1.FileName;
                PassValue("true", "true", fileName, "UTF-8");
                CheckFileCodeCheckBox("UTF-8");//勾选对应的文件编码复选框
                richTextBox1.Modified = false;

                //删除左上角文件名修改后添加的*号
                if (this.Text.Substring(this.Text.Length - 2) == " *" && saveFile == false)
                {
                    this.Text = this.Text.Remove(this.Text.Length - 2);
                    saveFile = true;
                }
            }
        }

        /// <summary>
        /// 根据文件扩展名和编码保存文件
        /// </summary>
        /// <param name="filename">文件路径</param>
        private void SaveFile(string filename, string code)
        {
            ext = Path.GetExtension(filename).ToLower();//获取文件的扩展名

            if (ext == ".rtf")
            {
                richTextBox1.SaveFile(filename, RichTextBoxStreamType.RichText);
                return;
            }
            else if (ext == ".doc" || ext == ".docx")
            {
                SaveWord(filename);
                return;
            }
            else if (ext == ".uni")
            {
                richTextBox1.SaveFile(filename, RichTextBoxStreamType.UnicodePlainText);
                return;
            }

            StreamWriter stream = new StreamWriter(filename, false, CodeNameToCodeStream(code));
            stream.Write(richTextBox1.Text);//写入到文件
            stream.Close();        //释放资源
        }



        //将编码名称转编码流名称
        private Encoding CodeNameToCodeStream(string code)
        {
            if (code == "UTF-8")
            {
                Encoding utf8 = new UTF8Encoding(false);
                return utf8;
            }
            else if (code == "带BOM的UTF-8")
            {
                Encoding utf8BOM = new UTF8Encoding(true);
                return utf8BOM;
            }
            else if (code == "UTF-16 LE")
            {
                return Encoding.Unicode;
            }
            else if (code == "UTF-16 BE")
            {
                return Encoding.BigEndianUnicode;
            }
            else if (code == "ASCII")
            {
                return Encoding.ASCII;
            }
            else //未知编码
                return Encoding.Default;
        }


        #region Word文档（打开/保存）
        //注意：要在解决方案资源管理器里找到Microsoft.Office.Interop.Word，右键属性，把嵌入互操作类型改为false
        private Microsoft.Office.Interop.Word.ApplicationClass app = null;
        private Microsoft.Office.Interop.Word.Document doc = null;
        private object missing = System.Reflection.Missing.Value;
        private object file;
        //先打开word文档，全选其中的内容并保存的剪切板中，最后在richTextBox中粘贴数据，并关闭文档
        private void OpenWord(string fileName)
        {
            file = fileName;
            app = new Microsoft.Office.Interop.Word.ApplicationClass();
            object readOnly = false;
            object isVisible = true;

            try
            {//打开Word文档
                doc = app.Documents.Open(ref file, ref missing, ref readOnly,
                 ref missing, ref missing, ref missing, ref missing, ref missing,
                 ref missing, ref missing, ref missing, ref isVisible, ref missing,
                 ref missing, ref missing, ref missing);

                doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据
                doc.ActiveWindow.Selection.Copy();//复制数据到剪切板
                richTextBox1.SelectAll();//全选richtextbox中的数据
                richTextBox1.Paste();//粘贴数据到richTextBox

                doc.Close(ref missing, ref missing, ref missing);
                app.Quit(ref missing, ref missing, ref missing);

                //更改字体样式
                if (toolStripComboBox2.SelectedItem == null || toolStripComboBox1.SelectedItem == null)
                    return;
                fontName = toolStripComboBox2.SelectedItem.ToString();
                pound = FileOperation.FontSizeAndPointConversion(toolStripComboBox1.SelectedItem.ToString());//将字号转为磅
                f = richTextBox1.Font != null ? richTextBox1.Font : richTextBox1.SelectionFont;
                richTextBox1.Font = new Font(fontName, pound, FontStyle.Regular);
            }
            catch (Exception ex)
            {
                error = string.Format($"打开{fileName}失败！\n原因：{ex.Message}");
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打开或创建word文档，全选word文档中的数据，然后全选richTextBox中的数据并复制到剪切板，然后粘贴到word文档中，最后保存word文档并关闭
        private void SaveWord(string fileName)
        {
            app = new Microsoft.Office.Interop.Word.ApplicationClass();
            file = fileName;
            object readOnly = false;
            object isVisible = true;

            try
            {
                if (File.Exists(fileName))//判断文件是否存在
                {//存在则直接打开
                    doc = app.Documents.Open(ref file, ref missing, ref readOnly,
                 ref missing, ref missing, ref missing, ref missing, ref missing,
                 ref missing, ref missing, ref missing, ref isVisible, ref missing,
                 ref missing, ref missing, ref missing);
                }
                else
                {//不存在则创建
                    doc = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                }
                if (richTextBox1.Text == "")
                    richTextBox1.Text = "\n";

                doc.ActiveWindow.Selection.WholeStory();//全选Word文档中的内容
                richTextBox1.SelectAll();//全选Richtextbox中的内容

                Clipboard.SetData(DataFormats.Rtf, richTextBox1.SelectedRtf);//复制RTF数据到剪贴板
                doc.ActiveWindow.Selection.Paste();//粘贴到Word文档

                doc.Save();//保存
                richTextBox1.Select(0, 0);
                doc.Close(ref missing, ref missing, ref missing);
                app.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                richTextBox1.Select(0, 0);
                error = string.Format($"保存{fileName}失败！\n原因：{ex.Message}");
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion



        //剪切
        private void 剪切UToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        //复制
        private void 复制CToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        //粘贴
        private void 粘贴PToolStripButton_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }



        //添加图片
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //设置文件类型
                openFileDialog1.Filter = "图像文件(*.JPEG、*.TIFF、*.BMP、*.GIF、*.PNG、*.WEPB)|*.JPEG;*.TIFF;*.RAW;*.BMP;*.GIF;*.PNG;*.WEPB|所有文件(*.*)|*.*";
                Bitmap bmp;

                if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName.Length > 0)
                {
                    bmp = new Bitmap(openFileDialog1.FileName);//转为Bitmap
                    Clipboard.SetDataObject(bmp);
                    DataFormats.Format format = DataFormats.GetFormat(DataFormats.Bitmap);//将图片发送到剪切板
                    if (richTextBox1.CanPaste(format))
                        richTextBox1.Paste(format);//粘贴
                }
            }
            catch (Exception ex)
            {
                error = string.Format($"图片：{openFileDialog1.FileName}添加失败！\n原因：{ex.Message}");
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //设置文件类型
                openFileDialog1.Filter = filter;
            }
        }



        //字体加粗
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new System.Drawing.Font(f, f.Style ^ FontStyle.Bold);
        }


        //字体倾斜
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new System.Drawing.Font(f, f.Style ^ FontStyle.Italic);
        }


        //添加下划线
        private void toolStripButton12_Click(object sender, EventArgs e)
        {

            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new System.Drawing.Font(f, f.Style ^ FontStyle.Underline);
        }


        //添加删除线
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new System.Drawing.Font(f, f.Style ^ FontStyle.Strikeout);
        }


        private bool up = false;
        //上标
        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionCharOffset = 0;
            if (!up)
            {
                up = true;
                //先取消下标
                down = false;
                toolStripButton14.BackColor = Color.FromKnownColor(KnownColor.Control);
               
                richTextBox1.SelectionCharOffset = 5;
                toolStripButton13.BackColor = Color.Chartreuse;
            }
            else
            {
                up = false;
                toolStripButton13.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            label1.Text = richTextBox1.SelectionCharOffset.ToString();
        }


        private bool down = false;
        //下标
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionCharOffset = 0;

            if (!down)
            {
                down = true;
                //先取消上标
                up = false;
                toolStripButton13.BackColor = Color.FromKnownColor(KnownColor.Control);

                richTextBox1.SelectionCharOffset = -5;
                toolStripButton14.BackColor = Color.Chartreuse;
            }
            else
            {
                down = false;
                toolStripButton14.BackColor = Color.FromKnownColor(KnownColor.Control);
            }
            label1.Text = richTextBox1.SelectionCharOffset.ToString();
        }


        //设置字体颜色
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;//设置选中字体的颜色
        }


        //撤销更改
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();

            if (richTextBox1.Text == "" && this.Text.Substring(this.Text.Length - 2) == " *" && saveFile == false)
            {
                this.Text = this.Text.Remove(this.Text.Length - 2);
                saveFile = true;
            }
        }


        //恢复撤销的更改
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }


        //左对齐
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }


        //居中
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }


        //右对齐
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }


        //增大一号字体
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            IncreaseFont(true, "A");
        }


        //减小一号字体
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            IncreaseFont(false, "A");
        }

        private int j;
        private void IncreaseFont(bool big, string mode)
        {
            //判断是否选中文字，选中文字则只把选中的文字放大缩小
            if (richTextBox1.SelectionLength > 0 && mode == "A")
            {
                if (big)
                {
                    if ((toolStripComboBox1.SelectedIndex + 1) >= fontSize.Length)
                        return;
                    j = toolStripComboBox1.SelectedIndex + 1;
                }
                else
                {
                    if ((toolStripComboBox1.SelectedIndex - 1) < 0)
                        return;
                    j = toolStripComboBox1.SelectedIndex - 1;
                }

                //将字号转为磅
                float pound = FileOperation.FontSizeAndPointConversion(fontSize[j]);

                f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
                richTextBox1.SelectionFont = new Font(f.FontFamily, pound, f.Style);
                toolStripComboBox1.SelectedItem = fontSize[j];
            }
            else
            {//否则全局放大缩小
                float zoom = richTextBox1.ZoomFactor;
                if (big)
                    zoom += 0.1F;
                else
                    zoom -= 0.1F;

                string str = zoom.ToString();
                zoom = (float)Convert.ToDouble(str);

                toolStripStatusLabel3.Text = ((int)(zoom * 100)).ToString() + "%";

                if (zoom <= 0.1F || zoom >= 5.0F)
                    return;
                richTextBox1.ZoomFactor = zoom;
            }
        }


        //设置字体样式
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox2.SelectedItem == null)
                return;

            fontName = toolStripComboBox2.SelectedItem.ToString();

            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new Font(fontName, f.Size, f.Style);
        }


        //设置字体大小
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem == null)
                return;

            string size = toolStripComboBox1.SelectedItem.ToString().Trim();
            pound = FileOperation.FontSizeAndPointConversion(size);//将字号转为磅

            f = richTextBox1.SelectionFont == null ? richTextBox1.Font : richTextBox1.SelectionFont;
            richTextBox1.SelectionFont = new Font(f.FontFamily, pound, f.Style);
        }


        //全选
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }


        //删除
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
        }


        //退出
        private void 退出XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1_FormClosing(null, null);
        }



        #region 打印
        //打印
        private void 打印PToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能暂未开放，尽请期待！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //打印页面设置
        private void 页面设置ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("此功能暂未开放，尽请期待！", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion


        //用户更改richTextBox的内容时在窗口左上角添加*号
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.Text.Substring(this.Text.Length - 1) != "*" && changeNumber >= 1)
            {
                if (!openFile)//判断是否未打开文件
                    this.Text = "无标题 *";
                else
                    this.Text += " *";
                saveFile = false;
            }
            changeNumber++;
        }



        //自动换行复选框（打勾/取消打勾）位置：格式-自动换行
        private void 自动换行toolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                ((ToolStripMenuItem)sender).Checked = false;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedBoth;//始终显示水平和垂直滚动条
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;//始终显示垂直滚动条
            }

            richTextBox1.WordWrap = ((ToolStripMenuItem)sender).Checked;
            cf.wordWrap = new StringBuilder(richTextBox1.WordWrap.ToString());
        }



        //隐藏窗体底部状态栏
        private void 隐藏状态栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
                ((ToolStripMenuItem)sender).Checked = false;
            else
                ((ToolStripMenuItem)sender).Checked = true;

            statusStrip1.Visible = ((ToolStripMenuItem)sender).Checked;
            cf.statusBar = new StringBuilder(statusStrip1.Visible.ToString());
        }



        private void 从右到左的顺序RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.RightToLeft == RightToLeft.No)
                richTextBox1.RightToLeft = RightToLeft.Yes;
            else
                richTextBox1.RightToLeft = RightToLeft.No;
        }


        //向richTextBox插入时间
        private void 插入时间DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(DateTime.Now.ToString());
        }


        //每隔1S刷新一次时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            时间toolStripStatusLabel1.Text = "当前时间：" + DateTime.Now.ToString();
        }


        //修改字体样式
        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionFont = fontDialog1.Font;
        }



        //右下角显示当前光标所在行和列
        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            //得到从richtextbox第1个字符开始到光标行的第1个字符索引
            int index = richTextBox1.GetFirstCharIndexOfCurrentLine();
            //得到光标行的行号,第1行从0开始计算
            int row = richTextBox1.GetLineFromCharIndex(index) + 1;
            //用光标当前所在索引减去当前行第一个字符的索引=光标所在的列数
            int column = richTextBox1.SelectionStart - index + 1;
            行列toolStripStatusLabel2.Text = string.Format("第{0}行，第{1}列", row, column);
        }


        private FindAndReplace findAndReplace = null;
        //查找和替换文件
        private void 查找和替换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //判断“查找和替换窗口”是否打开
            if (findAndReplace == null || findAndReplace.IsDisposed)
            {//未打开就显示
                findAndReplace = new FindAndReplace(this, richTextBox1.SelectedText);
                findAndReplace.Show();
                AddOwnedForm(findAndReplace);
            }
            else
                findAndReplace.Activate();//打开则激活“查找和替换窗口”
        }



        //支持拖动打开文件
        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            //判断是否启用“当打开的文件是rtf、doc、docx格式时，支持文本、图片和其它数据的拖放”这个功能
            if (OpenRichAutoDragDrop(fileDrag, ext))
                return;//如果启用则返回，避免打开拖动到窗体的文件

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All; //表明是所有类型的数据，如文件路径
        }
        //拖动打开文件
        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            //判断是否启用“当打开的文件是rtf、doc、docx格式时，支持文本、图片和其它数据的拖放”这个功能
            if (OpenRichAutoDragDrop(fileDrag, ext))
                return;//如果启用则返回，避免打开拖动到窗体的文件

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            try
            {
                string[] pathArry = (string[])e.Data.GetData(DataFormats.FileDrop);
                fileName = pathArry[0];
                ext = Path.GetExtension(fileName).ToLower();//获取文件的扩展名

                string code = FileOperation.GetEncoding(fileName);//获取文件编码
                Open(fileName, code);

                PassValue("true", "true", fileName, code);
                richTextBox1.Modified = false;
                saveFile = true;
                changeNumber = 0;
            }
            catch (Exception ex)
            {
                error = string.Format($"打开{fileName}错误！\n原因：{ex.Message}");
                MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //是否启用文件拖动（此项为true时，当当打开的文件是rtf、doc、docx格式时，支持文件拖动）
        private void 文件拖放TToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
                ((ToolStripMenuItem)sender).Checked = false;
            else
                ((ToolStripMenuItem)sender).Checked = true;

            fileDrag = ((ToolStripMenuItem)sender).Checked;
            OpenRichAutoDragDrop(fileDrag, ext); //当打开的文件是rtf、doc、docx格式时，是否启用文本、图片和其它数据的拖放
        }


        //通过LCtrl和鼠标滚轮缩小或放大richTextBox
        private void richTextBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {
                if (e.Delta > 0)
                    IncreaseFont(true, "Useless");
                else
                    IncreaseFont(false, "Useless");
            }
        }


        //恢复默认缩放
        private void 恢复默认缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = "100%";
            richTextBox1.ZoomFactor = 1.0F;
        }


        //恢复默认字体大小
        private void 恢复默认字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Font != null)
            {
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, 12F, richTextBox1.Font.Style);
                int num;
                if ((num = Array.LastIndexOf(fontSize, "12")) != -1)
                    toolStripComboBox1.SelectedIndex = num;
                else
                    toolStripComboBox1.SelectedIndex = 0;
            }
        }



        //重新用xx编码打开文件
        private string ToolName;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!openFile)//如果没有打开文件就不执行下面的代码
                return;

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "openASCIIToolStripMenuItem":
                    ToolName = "ANSI";
                    break;
                case "openUTF8ToolStripMenuItem":
                    ToolName = "UTF-8";
                    break;
                case "open带BOM的UTF8ToolStripMenuItem":
                    ToolName = "带BOM的UTF-8";
                    break;
                case "openUTF16LEToolStripMenuItem":
                    ToolName = "UTF-16 LE";
                    break;
                case "openUTF16BEToolStripMenuItem":
                    ToolName = "AUTF-16 BE";
                    break;
                default:
                    ToolName = "Default";
                    break;
            }
            Open(fileName, ToolName);

            //先把所有复选框取消打勾
            openASCIIToolStripMenuItem.Checked = false;
            openUTF8ToolStripMenuItem.Checked = false;
            open带BOM的UTF8ToolStripMenuItem.Checked = false;
            openUTF16LEToolStripMenuItem.Checked = false;
            openUTF16BEToolStripMenuItem.Checked = false;
            openNet默认ToolStripMenuItem.Checked = false;
            //把点击的复选框打勾
            ((ToolStripMenuItem)sender).Checked = true;
        }


        //用xx编码保存文件
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Name)
            {
                case "saveASCIIToolStripMenuItem":
                    ToolName = "ANSI";
                    break;
                case "saveUTF8ToolStripMenuItem":
                    ToolName = "UTF-8";
                    break;
                case "save带BOM的UTF8ToolStripMenuItem":
                    ToolName = "带BOM的UTF-8";
                    break;
                case "saveUTF16LEToolStripMenuItem":
                    ToolName = "UTF-16 LE";
                    break;
                case "saveUTF16BEToolStripMenuItem":
                    ToolName = "UTF-16 BE";
                    break;
                default:
                    ToolName = "Default";
                    break;
            }

            if (!openFile)
            {//如果没有打开文件，就显示另存为对话框
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SaveFile(fileName, ToolName);
                    this.Text = fileName = saveFileDialog1.FileName; //把保存的文件名显示到窗口左上角标题

                    richTextBox1.Modified = false;
                    openFile = true;
                    saveFile = true;
                }
            }
            else
                SaveFile(fileName, ToolName);

            文本编码toolStripStatusLabel5.Text = ToolName;//将文件编码显示在窗口右下角
        }


        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //加载窗体时从配置文件读取窗体上次关闭前的大小和位置
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //初始化结构体
                InitializeStruct();


                //如果配置文件存在，读取配置文件
                if (File.Exists(cf.iniPath))
                {
                    //读取“自动换行”项的值
                    GetPrivateProfileString(cf.section, cf.key1.ToString(), cf.err, cf.wordWrap, cf.MAX, cf.iniPath);
                    //读取“显示状态栏”项的值
                    GetPrivateProfileString(cf.section, cf.key2.ToString(), cf.err, cf.statusBar, cf.MAX, cf.iniPath);
                    //读取“右上角显示高和宽”项的值
                    GetPrivateProfileString(cf.section, cf.key3.ToString(), cf.err, cf.heightAndWidth, cf.MAX, cf.iniPath);
                    //读取“文件拖放”项的值
                    GetPrivateProfileString(cf.section, cf.key4.ToString(), cf.err, cf.Fd, cf.MAX, cf.iniPath);
                    //读取“不记住上次关闭时的大小”项的值
                    GetPrivateProfileString(cf.section, cf.key5.ToString(), cf.err, cf.RestoreFormSize, cf.MAX, cf.iniPath);
                    //读取“不记住上次关闭时的位置”项的值
                    GetPrivateProfileString(cf.section, cf.key6.ToString(), cf.err, cf.RestoreFormPosition, cf.MAX, cf.iniPath);
                    //读取窗口高度
                    GetPrivateProfileString(cf.section, cf.key7.ToString(), cf.err, cf.height, cf.MAX, cf.iniPath);
                    //读取窗口宽度
                    GetPrivateProfileString(cf.section, cf.key8.ToString(), cf.err, cf.width, cf.MAX, cf.iniPath);
                    //读取窗口x坐标
                    GetPrivateProfileString(cf.section, cf.key9.ToString(), cf.err, cf.x, cf.MAX, cf.iniPath);
                    //读取窗口y坐标
                    GetPrivateProfileString(cf.section, cf.key10.ToString(), cf.err, cf.y, cf.MAX, cf.iniPath);

                    if (RErr(cf.wordWrap) || RErr(cf.statusBar) || RErr(cf.heightAndWidth) || RErr(cf.Fd)
                        || RErr(cf.RestoreFormSize) || RErr(cf.RestoreFormPosition) || RErr(cf.height)
                        || RErr(cf.width) || RErr(cf.x) || RErr(cf.y))
                        return;

                    //自动换行
                    if (Convert.ToBoolean(cf.wordWrap.ToString()) == true)
                    {
                        自动换行toolStripMenuItem.Checked = true;
                        richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;//始终显示垂直滚动条
                    }
                    else
                    {
                        自动换行toolStripMenuItem.Checked = false;
                        richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedBoth;//始终显示水平和垂直滚动条
                    }

                    //显示状态栏
                    if (Convert.ToBoolean(cf.statusBar.ToString()) == true)
                    {
                        隐藏状态栏ToolStripMenuItem.Checked = true;
                        statusStrip1.Visible = true;
                    }
                    else
                    {
                        隐藏状态栏ToolStripMenuItem.Checked = false;
                        statusStrip1.Visible = false;
                    }

                    //显示窗体高和宽
                    if (Convert.ToBoolean(cf.heightAndWidth.ToString()) == true)
                    {
                        右上角显示高和宽ToolStripMenuItem.Checked = true;
                        label1.Visible = true;
                    }
                    else
                    {
                        右上角显示高和宽ToolStripMenuItem.Checked = false;
                        label1.Visible = false;
                    }

                    //启用文件拖放
                    if (Convert.ToBoolean(cf.Fd.ToString()))
                    {
                        文件拖放TToolStripMenuItem.Checked = true;
                        fileDrag = true;
                    }
                    else
                    {
                        文件拖放TToolStripMenuItem.Checked = false;
                        fileDrag = false;
                    }

                    //恢复窗体上次关闭时的大小
                    if (Convert.ToBoolean(cf.RestoreFormSize.ToString()) == false)
                    {
                        this.Width = Convert.ToInt32(cf.width.ToString());
                        this.Height = Convert.ToInt32(cf.height.ToString());
                    }
                    else
                        不记住上次关闭时的大小ToolStripMenuItem.Checked = true;

                    //恢复窗体上次关闭时的位置
                    if (Convert.ToBoolean(cf.RestoreFormPosition.ToString()) == false)
                        this.Location = new Point(Convert.ToInt32(cf.x.ToString()), Convert.ToInt32(cf.y.ToString()));
                    else
                        不记住上次关闭时的位置ToolStripMenuItem.Checked = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool RErr(StringBuilder str)
        {
            if (str.ToString().ToLower() == "error")
                return true;
            else
                return false;

        }

        //退出
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!saveFile)//判断用户上次编辑后是否保存，如未保存提示保存
            {
                DialogResult result = MessageBox.Show("是否保存更改？", "文本编辑器", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    保存SToolStripButton_Click(null, null);
                else if (result == DialogResult.Cancel)
                    e.Cancel = true;//取消关闭窗口
                Environment.Exit(0);
            }

            try
            {
                //判断配置文件目录是否存在
                if (!Directory.Exists(cf.iniDirectory))
                    Directory.CreateDirectory(cf.iniDirectory);

                WritePrivateProfileString(cf.section, cf.key1.ToString(), 自动换行toolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key2.ToString(), 隐藏状态栏ToolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key3.ToString(), 右上角显示高和宽ToolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key4.ToString(), 文件拖放TToolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key5.ToString(), 不记住上次关闭时的大小ToolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key6.ToString(), 不记住上次关闭时的位置ToolStripMenuItem.Checked.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key7.ToString(), this.Height.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key8.ToString(), this.Width.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key9.ToString(), this.Location.X.ToString(), cf.iniPath);
                WritePrivateProfileString(cf.section, cf.key10.ToString(), this.Location.Y.ToString(), cf.iniPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //按下LCtrl并点击超链接时打开链接
        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //判断点击时LCtrl键是否按下
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
                System.Diagnostics.Process.Start(e.LinkText);
        }


        private void 窗体恢复默认大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size newSize = new Size(765, 505);
            this.Size = newSize;
        }


        //改变窗口大小时将宽和高显示在lable控件上
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            label1.Text = string.Format(this.Width + "x" + this.Height);
        }

        private void 始终显示在最前方ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
            {
                ((ToolStripMenuItem)sender).Checked = false;
                this.TopMost = false;
            }
            else
            {
                ((ToolStripMenuItem)sender).Checked = true;
                this.TopMost = true;
            }
        }

        private void 右上角显示高和宽ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!((ToolStripMenuItem)sender).Checked)
                ((ToolStripMenuItem)sender).Checked = true;
            else
                ((ToolStripMenuItem)sender).Checked = false;

            label1.Visible = ((ToolStripMenuItem)sender).Checked;
            cf.heightAndWidth = new StringBuilder(label1.Visible.ToString());
        }


        //当打开窗体时不恢复上次关闭前的大小
        private void 不记住上次关闭时的大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
                ((ToolStripMenuItem)sender).Checked = false;
            else
                ((ToolStripMenuItem)sender).Checked = true;

            cf.RestoreFormSize = new StringBuilder(((ToolStripMenuItem)sender).Checked.ToString());
        }


        //当打开窗体时不恢复上次关闭前的位置
        private void 不记住上次关闭时的位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked)
                ((ToolStripMenuItem)sender).Checked = false;
            else
                ((ToolStripMenuItem)sender).Checked = true;

            cf.RestoreFormPosition = new StringBuilder(((ToolStripMenuItem)sender).Checked.ToString());
        }
    }




    class FileOperation
    {
        /// <summary>
        /// 字号转换磅
        /// </summary>
        /// <param name="size">字号</param>
        /// <returns></returns>
        public static float FontSizeAndPointConversion(string size)
        {
            switch (size)
            {
                case "初号": return 42f;
                case "小初": return 36f;
                case "一号": return 26f;
                case "小一": return 24f;
                case "二号": return 22f;
                case "小二": return 18f;
                case "三号": return 16f;
                case "小三": return 15f;
                case "四号": return 14f;
                case "小四": return 12f;
                case "五号": return 10.5f;
                case "小五": return 9f;
                case "六号": return 7.5f;
                case "小六": return 6.5f;
                case "七号": return 5.5f;
                case "八号": return 5f;
                default: return (float)Convert.ToDouble(size);

            }
        }


        /// <summary>
        /// 获取当前系统所有字体
        /// </summary>
        /// <returns></returns>
        public static string[] GetSystemFont()
        {
            InstalledFontCollection font = new InstalledFontCollection();
            string[] arr = new string[font.Families.Length];

            for (int i = 0; i < arr.Length; i++)
                arr[i] = font.Families[i].Name.ToString();

            return arr;
        }




        /// <summary>
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型 
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>返回string类型的文本编码</returns>
        public static string GetEncoding(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            string str = GetType(fs);
            fs.Close();
            return str;
        }

        /// <summary> 
        /// 通过给定的文件流，判断文件的编码类型 
        /// </summary> 
        /// <param name="fs">文件流</param> 
        /// <returns>文件的编码类型</returns> 
        public static string GetType(FileStream fs)
        {
            BinaryReader binary = new BinaryReader(fs, Encoding.Default);

            int len = fs.Length > 10485760 ? 10485760 : (int)fs.Length;
            byte[] buff = binary.ReadBytes(len);
            binary.Close();

            if (buff[0] == 0xEF && buff[1] == 0xBB && buff[2] == 0xBF)
            {
                return "带BOM的UTF-8";
            }
            else if (IsUTF8(buff))
            {
                return "UTF-8";
            }
            else if (buff[0] == 0xFE && buff[1] == 0xFF)
            {
                return "UTF-16 BE";
            }
            else if (buff[0] == 0xFF && buff[1] == 0xFE)
            {
                return "UTF-16 LE";
            }
            else
            {
                return "ANSI";
            }
        }


        /// <summary>
        /// 判断是否为不带BOM头的UTF-8编码 
        /// </summary>
        /// <param name="buff">要判断的字节数组</param>
        /// <returns></returns>
        private static bool IsUTF8(byte[] buff)
        {
            int count = 1; //计算当前正分析的字符应还有的字节数
            byte _Byte; //当前分析的字节

            for (int i = 0; i < buff.Length; i++)
            {
                _Byte = buff[i];

                if (count == 1)
                {
                    if (_Byte >= 0x80)
                    {
                        //判断当前
                        while (((_Byte <<= 1) & 0x80) != 0)
                            count++;

                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (count == 1 || count > 6)
                            return false;
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((_Byte & 0xC0) != 0x80)
                        return false;

                    count--;
                }
            }

            if (count > 1)
                return false;
            else
                return true;
        }
    }

}
