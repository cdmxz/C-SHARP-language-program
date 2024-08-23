using Notification.Wpf;
using Notification.Wpf.Constants;
using Notification.Wpf.Controls;
using System.Windows;
using System.Windows.Media;

namespace 鹰眼OCR_WPFCore.Helper
{
    /// <summary>
    /// 显示通知帮助类
    /// </summary>
    public static class NotifierHelper
    {
        private static readonly NotificationManager _notificationManager = new();

        private static readonly TimeSpan ExpirationTime = TimeSpan.FromSeconds(5);

        /// <summary>
        /// 通知显示区域
        /// 此名称需要和XAML中的 <notifications:NotificationArea x:Name="WindowArea"/> 
        /// x:Name一致
        /// </summary>
        private static readonly string _areaName = "WindowArea";

        static NotifierHelper()
        {
            NotificationConstants.MessagePosition = NotificationPosition.BottomCenter;
            NotificationConstants.NotificationsOverlayWindowMaxCount = 5;

            NotificationConstants.MinWidth = 400D;
            NotificationConstants.MaxWidth = 600D;

            NotificationConstants.FontName = "微软雅黑";
            NotificationConstants.TitleSize = 14;
            NotificationConstants.MessageSize = 14;
            NotificationConstants.MessageTextAlignment = TextAlignment.Left;
            NotificationConstants.TitleTextAlignment = TextAlignment.Left;

            NotificationConstants.DefaultBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#444444"));
            NotificationConstants.DefaultForegroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            NotificationConstants.InformationBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#409EFF"));
            NotificationConstants.SuccessBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#67C23A"));
            NotificationConstants.WarningBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E6A23C"));
            NotificationConstants.ErrorBackgroundColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F56C6C"));

        }

        /// <summary>
        /// 在MainWindow显示Toast通知
        /// </summary>
        /// <param name="type">通知类型</param>
        /// <param name="message">通知消息字符串</param>
        public static void Show(NotificationType type, string message)
        {
            string title = type switch
            {
                NotificationType.None => "",
                NotificationType.Information => "信息",
                NotificationType.Success => "成功",
                NotificationType.Warning => "警告",
                NotificationType.Error => "错误",
                NotificationType.Notification => "通知",
                _ => "",
            };

            var clickContent = new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type,
                TrimType = NotificationTextTrimType.NoTrim,
                CloseOnClick = true,
            };

            _notificationManager.Show(clickContent, _areaName, ExpirationTime);
        }

        /// <summary>
        /// 在指定的areaName显示Toast通知
        /// </summary>
        /// <param name="type">通知类型</param>
        /// <param name="message">通知消息字符串</param>
        /// <param name="areaName">区域Name，可以为""，为空时通知显示在任务栏上方</param>
        public static void Show(NotificationType type, string message, string areaName)
        {
            string title = type switch
            {
                NotificationType.None => "",
                NotificationType.Information => "信息",
                NotificationType.Success => "成功",
                NotificationType.Warning => "警告",
                NotificationType.Error => "错误",
                NotificationType.Notification => "通知",
                _ => "",
            };

            var clickContent = new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type,
                TrimType = NotificationTextTrimType.NoTrim,
                CloseOnClick = true,
            };

            _notificationManager.Show(clickContent, areaName, ExpirationTime);
        }


        public static void ShowInformation(string msg)
        {
            Show(NotificationType.Information, msg);
        }

        public static void ShowWarning(string msg)
        {
            Show(NotificationType.Warning, msg);
        }

        public static void ShowNotification(string msg)
        {
            Show(NotificationType.Notification, msg);
        }
        public static void ShowSuccess(string msg)
        {
            Show(NotificationType.Success, msg);
        }

        /// <summary>
        /// 在MainWindow显示异常通知
        /// </summary>
        public static void ShowError(Exception e)
        {
            ShowError(e.Message, _areaName);
        }

        /// <summary>
        /// 在MainWindow显示异常通知
        /// </summary>
        public static void ShowError(string msg)
        {
            ShowError(msg, _areaName);
        }


        /// <summary>
        /// 在指定的areaName显示异常通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="areaName">区域Name，可以为""，为空时通知显示在任务栏上方</param>
        public static void ShowError(string msg, string areaName)
        {
            var content = new NotificationContent
            {
                Title = "错误",
                Message = msg,
                Type = NotificationType.Error,
                TrimType = NotificationTextTrimType.NoTrim,
                RightButtonContent = "复制", // Right button content (string or what u want
                CloseOnClick = false, // Set true if u want close message when left mouse button click on message (base = true)
            };
            content.RightButtonAction = () =>
            {
                Clipboard.SetText(content.Formatter());
            };
            _notificationManager.Show(content, areaName, ExpirationTime);
        }


        public static string Formatter(this NotificationContent constants)
        {
            return $"时间：{DateTime.Now:G}{Environment.NewLine}标题：{constants.Title}{Environment.NewLine}消息：{constants.Message}{Environment.NewLine}";
        }
    }
}
