namespace 腾讯会议摸鱼助手.TengXunMeetingHelper
{
    /// <summary>
    /// 腾讯会议各个按钮的 相对坐标数据
    /// </summary>
    internal struct RelativePositionData
    {
        /// <summary>
        /// 离开会议按钮坐标
        /// </summary>
        public static Point LeaveButton { get; set; }
        /// <summary>
        /// 聊天按钮坐标
        /// </summary>
        public static Point ChatButton { get; set; }
        /// <summary>
        /// 聊天窗口文字输入框坐标
        /// </summary>
        public static Point InputBox { get; set; }
        /// <summary>
        /// 聊天窗口发送按钮坐标
        /// </summary>
        public static Point SendButton { get; set; }
        /// <summary>
        /// 改名确认按钮坐标
        /// </summary>
        public static Point OkButton { get; set; }
        /// <summary>
        /// 成员按钮坐标
        /// </summary>
        public static Point MemberButton { get; set; }
        /// <summary>
        /// 改名按钮坐标
        /// </summary>
        public static Point ChangeNameButton { get; set; }
    }
}
