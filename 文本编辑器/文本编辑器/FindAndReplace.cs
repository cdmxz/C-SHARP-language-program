using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 文本编辑器
{
    public partial class FindAndReplace : Form
    {
        private Form1 mainForm1;
        private string findStr = null; //要查找的字符串
        private string replaceStr = null;//要替换成的字符串
        private int findPos = 0;//当前查找的位置
        private int lastfindPos = 0;//上次的查找的位置
        private bool radioButton1Value, radioButton2Value;

        public FindAndReplace(Form1 from1, string str)
        {
            InitializeComponent();
            mainForm1 = from1;
            textBox1.Text = str;//将当前选定的字符显示到查找框
            radioButton1.Checked = false;
            radioButton2.Checked = true;
        }


        //查找下一个
        private void button1_Click(object sender, EventArgs e)
        {
            findStr = textBox1.Text;
            FindText(1, false, false);
        }

        //替换
        private void button2_Click(object sender, EventArgs e)
        {
            findStr = textBox1.Text;
            replaceStr = textBox2.Text;
            FindText(1, false, true);
            //替换
            if (mainForm1.richTextBox1.SelectedText.Length > 0)
                mainForm1.richTextBox1.SelectedText = replaceStr;
        }

        //向上查找复选框
        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked)//判断没有是否被选中
                radioButton1.Checked = false;
            else
            {
                radioButton1.Checked = true;
                button1.Text = "上一个(&F)";
            }
        }
        //向下查找复选框
        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (!radioButton2.Checked)//判断是否没有被选中
                radioButton2.Checked = false;
            else
            {
                radioButton2.Checked = true;
                button1.Text = "下一个(&F)";
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
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)//如果没有被选中
                checkBox2.Checked = false;
            else
                checkBox2.Checked = true;
        }

        //替换所有
        private void button3_Click(object sender, EventArgs e)
        {
            //备份原来的值
            radioButton1Value = radioButton1.Checked;
            radioButton2Value = radioButton2.Checked;
            //禁用向上查找复选框
            radioButton1.Checked = false;
            radioButton1.Enabled = false;
            //向下查找
            radioButton2.Checked = true;

            findStr = textBox1.Text;
            replaceStr = textBox2.Text;
            int i = 1;
            while (FindText(i, true, true))
            {
                i++;
                if (mainForm1.richTextBox1.SelectedText.Length > 0)
                    mainForm1.richTextBox1.SelectedText = replaceStr;
            }

            //启用向上查找复选框
            radioButton1.Enabled = true;
            //恢复原来的值
            radioButton1.Checked = radioButton1Value;
            radioButton2.Checked = radioButton2Value;
        }


        /// <summary>
        /// 在richtextbox中查找替换匹配的文本
        /// </summary>
        /// <returns></returns>
        private bool FindText(int number, bool loopSearch, bool replace)
        {
            label4.Enabled = false;
            label4.Text = "查找信息：";

            //如果是循环查找（替换所有）
            if (loopSearch)
            {
                if (radioButton2.Checked)//向下查找
                    findPos = mainForm1.richTextBox1.Text.IndexOf(findStr);
                else                     //向上查找
                    findPos = mainForm1.richTextBox1.Text.LastIndexOf(findStr);

                if (findPos == -1 && number == 1)//如果是第一次查找并且未找到
                {
                    label4.Enabled = true;
                    label4.Text = "未找到指定的文本";
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
                if (radioButton2.Checked) //向下查找
                {
                    if (checkBox2.Checked)//全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, findPos, mainForm1.richTextBox1.Text.Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, findPos, mainForm1.richTextBox1.Text.Length, RichTextBoxFinds.MatchCase);
                }
                else//向上查找
                {
                    if (checkBox2.Checked)//全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, findPos, mainForm1.richTextBox1.Text.Length, RichTextBoxFinds.MatchCase | RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, 0, findPos, RichTextBoxFinds.MatchCase | RichTextBoxFinds.Reverse);
                }
            }
            else//不匹配大小写
            {
                if (radioButton2.Checked) //向下查找
                {
                    if (checkBox2.Checked)//全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, findPos, mainForm1.richTextBox1.Text.Length, RichTextBoxFinds.WholeWord);
                    else//不全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, findPos, mainForm1.richTextBox1.Text.Length, RichTextBoxFinds.None);
                }
                else//向上查找
                {
                    if (checkBox2.Checked)//全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, 0, findPos, RichTextBoxFinds.WholeWord | RichTextBoxFinds.Reverse);
                    else//不全字匹配
                        findPos = mainForm1.richTextBox1.Find(findStr, 0, findPos, RichTextBoxFinds.Reverse);
                }
            }



            if (findPos == -1 && number == 1)//如果未找到
            {
                label4.Enabled = true;
                label4.Text = "未找到指定的文本";
                findPos = lastfindPos;//还原上次 查找的位置
                return false;
            }
            else if (findPos == -1)
            {
                findPos = lastfindPos;//还原上次查找的位置
                return false;
            }


            //向下查找并且不为替换模式
            if (radioButton2.Checked && !replace)
            {
                findPos += findStr.Length;
                if (findPos > mainForm1.richTextBox1.TextLength)
                    findPos = mainForm1.richTextBox1.TextLength;
            }

            mainForm1.Focus();//主窗体获得焦点
            lastfindPos = findPos;//将当前查找的位置保存，以便循环查找替换
            return true;
        }


    }
}