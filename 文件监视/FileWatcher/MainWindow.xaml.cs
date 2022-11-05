using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileWatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int totalChanges;
        private ObservableCollection<FileList> fileLists;// 数据列表
        private Status curStatus;        // 当前状态
        private FileSystemWatcher fileWatcher;

        // 当前状态
        enum Status
        {
            Start,
            Stop
        }


        public MainWindow()
        {
            InitializeComponent();

            fileWatcher = new FileSystemWatcher();
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fileWatcher.IncludeSubdirectories = true;// 包含子目录
                                                     // 订阅事件
            fileWatcher.Changed += FileWatcher_Changed;
            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.Deleted += FileWatcher_Deleted;
            fileWatcher.Renamed += FileWatcher_Renamed;
            fileWatcher.Error += FileWatcher_Error;

            // 数据列表
            fileLists = new ObservableCollection<FileList>();
            listView1.ItemsSource = fileLists;
            // 当前状态
            curStatus = Status.Stop;
            this.textBox_path.Text = @"C:\";
            totalChanges = 0;
        }

        // 打开路径
        private void button_open_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonOpenFileDialog dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;// 选择文件夹
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    this.textBox_path.Text = dialog.FileName;
                dialog.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }

        // 启动
        private void Start()
        {
            try
            {
                curStatus = Status.Start;
                string path = this.textBox_path.Text;
                if (!Directory.Exists(path))
                    throw new Exception("路径无效！");
                fileWatcher.Path = path;
                textBlock_status.Text = "运行中";
                image_monitor.Source = new BitmapImage(new Uri(@"pack://application:,,,/Image/stop.png"));
                ellipse1.Fill = Brushes.DodgerBlue;
                button_monitor.ToolTip = "点击停止";
                fileWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 停止
        private void Stop()
        {
            try
            {
                curStatus = Status.Stop;
                textBlock_status.Text = "停止";
                image_monitor.Source = new BitmapImage(new Uri(@"pack://application:,,,/Image/start.png"));
                ellipse1.Fill = Brushes.LightGray;
                button_monitor.ToolTip = "点击启动";
                fileWatcher.EnableRaisingEvents = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 启动或停止 文件监测
        private void monitor_Click(object sender, RoutedEventArgs e)
        {
            if (curStatus == Status.Stop)
            {
                Start();
                this.textBox_path.IsEnabled = true;
            }
            else
            {
                Stop();
                this.textBox_path.IsEnabled = false;
            }
            e.Handled = true;
        }

        // 保存
        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.RestoreDirectory = true;
                dialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*,*";
                if (!(bool)dialog.ShowDialog())
                    return;
                using (FileStream fs = new FileStream(dialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string content;
                    for (int i = 0; i < listView1.Items.Count; i++)
                    {
                        var list = (FileList)listView1.Items[i];
                        content = string.Format($"时间：{list.Time}|操作：{list.Operation}|大小：{list.Size}|路径：{list.Path}");
                        if (!string.IsNullOrEmpty(list.NewPath))
                            content += $"|新路径：{list.NewPath}";
                        sw.WriteLine(content);
                        sw.Flush();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            e.Handled = true;
        }

        // 重置
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            Stop();
            fileLists.Clear();
        }

        // 打开文件或目录
        private void OpenFileOrDir(string path)
        {
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = true;
                    myProcess.StartInfo.FileName = path;
                    myProcess.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // 打开选中的文件所在的文件夹
        private void menuItem_openDir_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                var list = (FileList)listView1.SelectedItems[i];
                string path = string.IsNullOrEmpty(list.NewPath) ? list.Path : list.NewPath;
                OpenFileOrDir(Path.GetDirectoryName(path));
            }
            e.Handled = true;
        }

        // 打开选中的文件
        private void menuItem_openFile_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                var list = (FileList)listView1.SelectedItems[i];
                string path = string.IsNullOrEmpty(list.NewPath) ? list.Path : list.NewPath;
                OpenFileOrDir(path);
            }
            e.Handled = true;
        }

        // 文件属性
        [DllImport("FileProperty.dll", CharSet = CharSet.Unicode, EntryPoint = "ShowFileProperty",CallingConvention =CallingConvention.Winapi)]
        public static extern bool ShowFileProperty(string fileName);

        private void menuItem_fileProperty_Click(object sender, RoutedEventArgs e)
        {
            FileList list = (FileList)this.listView1.SelectedItem;
            if (list == null)
                return;
            string path = string.IsNullOrEmpty(list.NewPath) ? list.Path : list.NewPath;
            ShowFileProperty(path);
            e.Handled = true;
        }


        private void menuItem_undo_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.Undo();
            e.Handled = true;
        }

        private void menuItem_cut_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.Cut();
            e.Handled = true;
        }

        private void menuItem_copy_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.Copy();
            e.Handled = true;
        }

        private void menuItem_paste_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.Paste();
            e.Handled = true;
        }

        private void menuItem_delete_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.SelectedText = "";
            e.Handled = true;
        }

        private void menuItem_selectAll_Click(object sender, RoutedEventArgs e)
        {
            textBox_path.SelectAll();
            e.Handled = true;
        }

        // 复制选择的一行的内容
        private void menuItem_copyContent_Click(object sender, RoutedEventArgs e)
        {
            string content;
            for (int i = 0; i < listView1.SelectedItems.Count; i++)
            {
                var list = (FileList)listView1.SelectedItems[i];
                content = string.Format($"时间：{list.Time}|操作：{list.Operation}|大小：{list.Size}|路径：{list.Path}");
                if (!string.IsNullOrEmpty(list.NewPath))
                    content += $"|新路径：{list.NewPath}";
                Clipboard.SetText(content);
            }
            e.Handled = true;
        }

        // 获取文件大小的字符串
        private string GetFileSizeStr(string file)
        {
            string size = "未知";
            try
            {
                if (File.Exists(file))
                {
                    var info = new FileInfo(file);
                    if (info.Length < 1048576)
                        size = (info.Length / 1024.0).ToString("f2") + "KB";
                    else
                        size = (info.Length / 1048576.0).ToString("f2") + "MB";
                }
                return size;
            }
            catch
            {
                return size;
            }
        }

        // 监视出错
        private void FileWatcher_Error(object sender, ErrorEventArgs e)
        {
            UpdateListView(new FileList(DateTime.Now.ToString("G"), "错误", "未知", e.ToString(), ""));
        }

        // 文件重命名
        private void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            string size = GetFileSizeStr(e.FullPath);
            UpdateListView(new FileList(DateTime.Now.ToString("G"), GetStatusStr(e.ChangeType), size, e.OldFullPath, e.FullPath));
        }

        // 创建
        private void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(e.FullPath))
                return;
            string size = GetFileSizeStr(e.FullPath);
            UpdateListView(new FileList(DateTime.Now.ToString("G"), GetStatusStr(e.ChangeType), size, e.FullPath, ""));
        }

        // 删除
        private void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string size = GetFileSizeStr(e.FullPath);
            UpdateListView(new FileList(DateTime.Now.ToString("G"), GetStatusStr(e.ChangeType), size, e.FullPath, ""));
        }

        // 修改
        private void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            string size = GetFileSizeStr(e.FullPath);
            UpdateListView(new FileList(DateTime.Now.ToString("G"), GetStatusStr(e.ChangeType), size, e.FullPath, ""));
        }

        // 刷新ListView控件
        private void UpdateListView(FileList list)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(delegate
                {
                    fileLists.Add(list);
                    totalChanges++;
                    this.textBlock_numOfChange.Content = totalChanges.ToString();
                });
            }
            catch
            {

            }
        }

        // 获取修改状态字符串
        private string GetStatusStr(WatcherChangeTypes types)
        {
            switch (types)
            {
                case WatcherChangeTypes.Changed:
                    return "更改";
                case WatcherChangeTypes.Created:
                    return "创建";
                case WatcherChangeTypes.Deleted:
                    return "删除";
                case WatcherChangeTypes.Renamed:
                    return "重命名";
                default:
                    return "未知";
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Stop();
        }

        private void listView1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                FileList list = (FileList)this.listView1.SelectedItem;
                if (list == null)
                    return;
                string path = string.IsNullOrEmpty(list.NewPath) ? list.Path : list.NewPath;
                OpenFileOrDir(path);
            }
        }
    }


    class FileList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public FileList(string time, string operation, string size, string path, string newPath)
        {
            _time = time;
            _operation = operation;
            _size = size;
            _path = path;
            _newPath = newPath;
        }

        // 时间
        private string _time;
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("time"));
            }
        }

        // 操作（删除、创建等）
        private string _operation;
        public string Operation
        {
            get { return _operation; }
            set
            {
                _operation = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("operation"));
            }
        }

        // 文件大小
        private string _size;
        public string Size
        {
            get { return _size; }
            set
            {
                _size = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("size"));
            }
        }

        // 文件路径
        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("path"));
            }
        }

        // 文件重命名的时候的新路径
        private string _newPath;
        public string NewPath
        {
            get { return _newPath; }
            set
            {
                _newPath = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("newPath"));
            }
        }
    }
}