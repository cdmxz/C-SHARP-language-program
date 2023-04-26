using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace ToICO
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.Multiselect = true;
            openFileDialog1.DereferenceLinks = true;
            openFileDialog1.Filter = "图片 (*.jpg;*.jpeg;*.png;*.webp;*.bmp;*.gif)|*.jpg;*.jpeg;*.png;*.webp;*.bmp;*.gif";
            openFileDialog1.Title = "请选择图片文件...";
            openFileDialog1.FileName = "";

            folderBrowserDialog1.Description = "请选择文件夹...";
            folderBrowserDialog1.ShowNewFolderButton = true;

            checkedListBox1.CheckOnClick = true;

            // 拖动文件
            pictureBox1.AllowDrop = true;
            label4_tip.AllowDrop = true;

            comboBox_TargetFormat.SelectedIndex = 0;
        }


        private void button_open_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image?.Dispose();
                listView1.Items.Clear();
                toolStripStatusLabel1.Text = "状态：";
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                    return;

                if (openFileDialog1.FileNames.Length == 1)
                    ShowPicture(openFileDialog1.FileNames[0]);
                AddListViewItems(openFileDialog1.FileNames);
                toolStripStatusLabel1.Text += "一共" + openFileDialog1.FileNames.Length + "个文件";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }

        private void AddListViewItems(string[] files)
        {
            string[] info = new string[3];
            foreach (var file in files)
            {
                info[0] = Path.GetFileName(file);
                info[1] = GetFileSizeStr(file);
                info[2] = file;
                listView1.Items.Add(new ListViewItem(info, file));
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (listView1.Items.Count == 0)
                    throw new Exception("失败：请先打开文件。");

                if (!checkBox_noChangeSize.Checked)
                {
                    // 如果未选中 复选框列表中的复选框 或 自定义宽高有一项小于或等于0
                    if (checkedListBox1.CheckedItems.Count < 1 && (numericUpDown1_height.Value <= 0 || numericUpDown2_width.Value <= 0))
                        throw new Exception("失败：请选中“不改变大小” 或 设置自定义宽高。");
                }
                string savePath = null;
                // 是否保存到源文件夹
                if (!checkBox_SaveToSourceFolder.Checked)
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        savePath = folderBrowserDialog1.SelectedPath;
                    else
                        throw new Exception("保存失败。");
                }
                // 保存并转换图片
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    string file = listView1.Items[i].ImageKey;
                    if (savePath == null)
                        savePath = Path.GetDirectoryName(file);
                    ConvertImage(file, (string)comboBox_TargetFormat.SelectedItem, savePath);
                }
                toolStripStatusLabel1.Text = "保存成功。";
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }

        private void ConvertImage(string fileName, string destFormat, string savePath)
        {
            List<Size> sizes = new List<Size>();
            string destName;
            // 改变大小
            if (!checkBox_noChangeSize.Checked)
            {
                // 遍历打勾的复选框
                foreach (var ckItem in checkedListBox1.CheckedItems)
                    sizes.Add(SplitSize(ckItem.ToString(), 'x'));
                // 自定义大小
                if (numericUpDown1_height.Value > 0 && numericUpDown2_width.Value > 0)
                    sizes.Add(new Size((int)numericUpDown2_width.Value, (int)numericUpDown1_height.Value));
            }
            else // 不改变大小
                sizes.Add(new Size(0, 0));

            foreach (var item in sizes)
            {
                if (item.Width == 0 && item.Height == 0)
                    destName = savePath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "." + destFormat.ToLower();
                else
                    destName = savePath + "\\" + Path.GetFileNameWithoutExtension(fileName) + "_" + item.Width + "x" + item.Height + "." + destFormat.ToLower();
                ImageConvert.ConvertImage(fileName, destName, item, destFormat);
            }
        }


        private void button_clean_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
                toolStripStatusLabel1.Text = "清除成功。";
            }
            else
            {
                toolStripStatusLabel1.Text = "清除失败。";
            }
            listView1.Items.Clear();
            label4_tip.Visible = true;
        }


        private static Size SplitSize(string size, char ch)
        {
            string[] arr = size.Split(ch);
            int width = Convert.ToInt32(arr[0]);
            int height = Convert.ToInt32(arr[1]);
            return new Size(width, height);
        }


        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))// 只接受文件拖动
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }


        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
            listView1.Items.Clear();
            // 获得路径
            string fileName = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            AddListViewItems(new string[] { fileName });
            ShowPicture(fileName);
        }


        private void ShowPicture(string fileName)
        {
            try
            {
                pictureBox1.Image?.Dispose();
                using (Image img = Image.FromFile(fileName))
                {
                    pictureBox1.Image = (Image)img.Clone();// 防止文件被锁
                }
                label4_tip.Visible = false;
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = ex.Message;
            }
        }


        private void checkBox_topMost_Click(object sender, EventArgs e)
        {
            if (checkBox_topMost.Checked)
                checkBox_topMost.Checked = false;
            else
                checkBox_topMost.Checked = true;

            this.TopMost = checkBox_topMost.Checked;
        }


        private void checkBox_noChangeSize_Click(object sender, EventArgs e)
        {
            if (checkBox_noChangeSize.Checked)
                checkBox_noChangeSize.Checked = false;
            else
                checkBox_noChangeSize.Checked = true;

            checkedListBox1.Enabled = !checkBox_noChangeSize.Checked;
            groupBox1.Enabled = !checkBox_noChangeSize.Checked;
        }


        private void checkBox_SaveToSourceFolder_Click(object sender, EventArgs e)
        {
            ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
        }

        private string GetFileSizeStr(string file)
        {
            string sizeStr = "未知";
            try
            {
                double size = new FileInfo(file).Length;
                if (size < 1048576)
                    sizeStr = (size / 1024.0).ToString("f2") + "KB";
                else
                    sizeStr = (size / 1048576.0).ToString("f2") + "MB";
            }
            catch
            {
                return sizeStr;
            }
            return sizeStr;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            string file = listView1.SelectedItems[0].ImageKey;
            ShowPicture(file);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetItemChecked(3,true);
            checkedListBox1.SetItemChecked(5,true);
            checkedListBox1.SetItemChecked(6,true);
            checkedListBox1.SetItemChecked(8,true);
            checkedListBox1.SetItemChecked(9,true);
        }
    }
}
