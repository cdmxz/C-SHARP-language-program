using System.ComponentModel;
using 腾讯会议摸鱼助手.TengXunMeetingHelper;
using 腾讯会议摸鱼助手.Utils;

namespace 腾讯会议摸鱼助手
{
    internal class GUIData : INotifyPropertyChanged
    {
        private bool _autoEnterMeeting;
        private bool _autoReply;
        private bool _autoLeave;
        private bool _autoSendReminder;
        private string _autoReplyText = string.Empty;
        private string _nameKeyword = string.Empty;
        private string _leaveKeyword = string.Empty;
        private string _sendReminderKeyword = string.Empty;
        private string _listboxNameText = string.Empty;
        private string _Uuid = string.Empty;
        private string _Uid = string.Empty;


        // 坐标
        private string _chatButtonPosition = string.Empty;
        private string _inputBoxPosition = string.Empty;
        private string _sendButtonPosition = string.Empty;
        private string _memberButtonPosition = string.Empty;
        private string _changeNameButtonPosition = string.Empty;
        private string _okButtonPosition = string.Empty;
        private string _leaveButtonPosition = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool AutoEnterMeeting
        {
            get { return _autoEnterMeeting; }
            set
            {
                _autoEnterMeeting = value;
                // 通知其它绑定了同一属性的控件，数据源的值改变
                // 实现双向绑定
                NotifyPropertyChanged(nameof(AutoEnterMeeting));// 这里是属性名
            }
        }

        public bool AutoReply
        {
            get { return _autoReply; }
            set
            {
                _autoReply = value;
                NotifyPropertyChanged(nameof(AutoReply));
            }
        }
        public bool AutoLeave
        {
            get { return _autoLeave; }
            set
            {
                _autoLeave = value;
                NotifyPropertyChanged(nameof(AutoLeave));
            }
        }
        public bool AutoSendReminder
        {
            get { return _autoSendReminder; }
            set
            {
                _autoSendReminder = value;
                NotifyPropertyChanged(nameof(AutoSendReminder));
            }
        }

        public string AutoReplyText
        {
            get { return _autoReplyText; }
            set
            {
                _autoReplyText = value;
                NotifyPropertyChanged(nameof(AutoReplyText));
            }
        }
        public string NameKeyword
        {
            get { return _nameKeyword; }
            set
            {
                _nameKeyword = value;
                NotifyPropertyChanged(nameof(NameKeyword));
            }
        }

        public string LeaveKeyword
        {
            get { return _leaveKeyword; }
            set
            {
                _leaveKeyword = value;
                NotifyPropertyChanged(nameof(LeaveKeyword));
            }
        }
        public string SendReminderKeyword
        {
            get { return _sendReminderKeyword; }
            set
            {
                _sendReminderKeyword = value;
                NotifyPropertyChanged(nameof(SendReminderKeyword));
            }
        }

        /// <summary>
        /// textbox里的名字
        /// </summary>
        public string ListboxNameText
        {
            get { return _listboxNameText; }
            set
            {
                _listboxNameText = value;
                NotifyPropertyChanged(nameof(ListboxNameText));
            }
        }

        public string Uuid
        {
            get => _Uuid;
            set
            {
                _Uuid = value;
                NotifyPropertyChanged(nameof(Uuid));
            }
        }
        public string Uid
        {
            get => _Uid;
            set
            {
                _Uid = value;
                NotifyPropertyChanged(nameof(Uid));
            }
        }

        // 各个按钮坐标
        public string ChatButtonPosition
        {

            get { return _chatButtonPosition; }
            set
            {
                _chatButtonPosition = value;
                RelativePositionData.ChatButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(ChatButtonPosition));
            }
        }
        public string InputBoxPosition
        {

            get { return _inputBoxPosition; }
            set
            {
                _inputBoxPosition = value;
                RelativePositionData.InputBox = Util.PointParse(value);
                NotifyPropertyChanged(nameof(InputBoxPosition));
            }
        }
        public string SendButtonPosition
        {

            get { return _sendButtonPosition; }
            set
            {
                _sendButtonPosition = value;
                RelativePositionData.SendButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(SendButtonPosition));
            }
        }
        public string MemberButtonPosition
        {

            get { return _memberButtonPosition; }
            set
            {
                _memberButtonPosition = value;
                RelativePositionData.MemberButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(MemberButtonPosition));
            }
        }
        public string ChangeNameButtonPosition
        {

            get { return _changeNameButtonPosition; }
            set
            {
                _changeNameButtonPosition = value;
                RelativePositionData.ChangeNameButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(ChangeNameButtonPosition));
            }
        }
        public string OkButtonPosition
        {

            get { return _okButtonPosition; }
            set
            {
                _okButtonPosition = value;
                RelativePositionData.OkButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(OkButtonPosition));
            }
        }
        public string LeaveButtonPosition
        {

            get { return _leaveButtonPosition; }
            set
            {
                _leaveButtonPosition = value;
                RelativePositionData.LeaveButton = Util.PointParse(value);
                NotifyPropertyChanged(nameof(LeaveButtonPosition));
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
