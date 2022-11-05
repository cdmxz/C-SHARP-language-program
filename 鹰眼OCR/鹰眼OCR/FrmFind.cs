using System;
using System.Drawing;
using System.Windows.Forms;

namespace 鹰眼OCR
{
    public partial class FrmFind : Form
    {
        private string findStr; //要查找的字符串
        private string replaceStr;//要替换成的字符串
        private int findPos = 0;//当前查找的位置
        private int lastfindPos = 0;//上次的查找的位置
        private bool radioButton_UpValue, radioButton_DownValue;

        public Point Position;

        public MainForm.RichTextBoxTextDelegate RichTextBoxText;

        public MainForm.RichTextBoxSelectedTextDelegate RichTextBoxSelectedText;

        public MainForm.RichTextBoxFindDelegate RichTextBoxFind;

        public FrmFind(string str)
        {
            InitializeComponent();
            textBox_Find.Text = str;//将当前选定的字符显示到查找框
            radioButton_Up.Checked = false;
            radioButton_Down.Checked = true;
        }


        //查找下一个
        private void button_FindNext_Click(object sender, EventArgs e)
        {
            findStr = textBox_Find.Text;
            FindText(1, false, false);
        }

        //替换
        private void button_Replace_Click(object sender, EventArgs e)
        {
            findStr = textBox_Find.Text;
            replaceStr = textBox_Replace.Text;
            FindText(1, false, true);
            //替换
            string selectedText = RichTextBoxSelectedText?.Invoke();
            if (selectedText.Length > 0)
                RichTextBoxSelectedText?.Invoke(replaceStr);
        }

        //向上查找复选框
        private void radioButton_Up_Click(object sender, EventArgs e)
        {
            if (!radioButton_Up.Checked)//判断没有是否被选中
                radioButton_Up.Checked = false;
            else
            {
                radioButton_Up.Checked = true;
                button_FindNext.Text = "上一个(&F)";
            }
        }
        //向下查找复选框
        private void radioButton_Down_Click(object sender, EventArgs e)
        {
            if (!radioButton_Down.Checked)//判断是否没有被选中
                radioButton_Down.Checked = false;
            else
            {
                radioButton_Down.Checked = true;
                button_FindNext.Text = "下一个(&F)";
            }
        }


        //区分大小写复选框
        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)//如果没有被选中
                checkBox1.Checked = false;
            else
                checkBox1.Checked = true;
        }

        //全字匹配复选框
        private void checkBox_Matching_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox_Matching.Checked)//如果没有被选中
                checkBox_Matching.Checked = false;
            else
                checkBox_Matching.Checked = true;
        }

        //替换所有
        private void button_ReplaceAll_Click(object sender, EventArgs e)
        {
            //备份原来的值
            radioButton_UpValue = radioButton_Up.Checked;
            radioButton_DownValue = radioButton_Down.Checked;
            //禁用向上查找复选框
            radioButton_Up.Checked = false;
            radioButton_Up.Enabled = false;
            //向下查找
            radioButton_Down.Checked = true;

            findStr = textBox_Find.Text;
            replaceStr = textBox_Replace.Text;
            int i = 1;
            while (FindText(i, true, true))
            {
                i++;
                string selectedText = RichTextBoxSelectedText?.Invoke();
                if (selectedText.Length > 0)
                    RichTextBoxSelectedText?.Invoke(replaceStr);
            }

            //启用向上查找复选框
            radioButton_Up.Enabled = true;
            //恢复原来的值
            radioButton_Up.Checked = radioButton_UpValue;
            radioButton_Down.Checked = radioButton_DownValue;
        }

        private void FrmFind_Load(object sender, EventArgs e)
        {
            this.Location = Position;
        }

        private void FrmFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
                button_FindNext_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }


        /// <summary>
        /// 在richtextbox中查找替换匹配的文本
        /// </summary>
        /// <returns></returns>
        private bool FindText(int number, bool loopSearch, bool replace)
        {
            label_Msg.Enabled = false;
            label_Msg.Text = "查找信息：";

            //如果是循环查找（替换所有）
            if (loopSearch)
            {
                string text = RichTextBoxText?.Invoke();
                if (radioButton_Down.Checked)//向下查找
                    findPos = text.IndexOf(findStr);
                else                     //向上查找
                    findPos = text.LastIndexOf(findStr);

                if (findPos == -1 && number == 1)//如果是第一次查找并且未找到
                {
                    label_Msg.Enabled = true;
                    label_Msg.Text = "未找到指定的文本";
                    findPos = lastfindPos;//还原上次查找的位置
                    return false;
                }
                else if (findPos == -1)
                {
                    findPos = lastfindPos;//还原上次查找的位置
                    return false;
                }
            }

            if (checkBox1.Checked)//匹配大小写
            {
                if (radioButton_Down.Checked) //向下查找
                {
                    if (checkBox_Matching.Checked)//全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, findPos, RichTextBoxText.Invoke().Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, findPos, RichTextBoxText.Invoke().Length, RichTextBoxFinds.MatchCase);
                }
                else//向上查找
                {
                    if (checkBox_Matching.Checked)//全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, findPos, RichTextBoxText.Invoke().Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, 0, findPos, RichTextBoxFinds.MatchCase | RichTextBoxFinds.Reverse);
                }
            }
            else//不匹配大小写
            {
                if (radioButton_Down.Checked) //向下查找
                {
                    if (checkBox_Matching.Checked)//全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, findPos, RichTextBoxText.Invoke().Length, RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, findPos, RichTextBoxText.Invoke().Length, RichTextBoxFinds.None);
                }
                else//向上查找
                {
                    if (checkBox_Matching.Checked)//全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, 0, findPos, RichTextBoxFinds.WholeWord | RichTextBoxFinds.Reverse);
                    else//不全字匹配
                        findPos = RichTextBoxFind.Invoke(findStr, 0, findPos, RichTextBoxFinds.Reverse);
                }
            }

            if (findPos == -1 && number == 1)//如果未找到
            {
                label_Msg.Enabled = true;
                label_Msg.Text = "未找到指定的文本";
                findPos = lastfindPos;//还原上次 查找的位置
                return false;
            }
            else if (findPos == -1)
            {
                findPos = lastfindPos;//还原上次查找的位置
                return false;
            }

            //向下查找并且不为替换模式
            if (radioButton_Down.Checked && !replace)
            {
                findPos += findStr.Length;
                if (findPos > RichTextBoxText.Invoke().Length)
                    findPos = RichTextBoxText.Invoke().Length;
            }

          //  mainForm1.Focus();//主窗体获得焦点
            lastfindPos = findPos;//将当前查找的位置保存，以便循环查找替换
            return true;
        }


    }
}
