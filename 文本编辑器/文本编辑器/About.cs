using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 文本编辑器
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        //打开网址
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/fuhohua");
        }

        private void About_Load(object sender, EventArgs e)
        {
            //获取最后编译时间
            label1.Text = "最后编译时间：" + File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyy-dd-MM HH:mm:ss");
            //int x = Application.OpenForms["Form1"].Right;
            //int y = Application.OpenForms["Form1"].Top;
            //this.Location = new Point(x-550, y+150);//让当前窗口显示在主窗口的中间
        }
    }
}
