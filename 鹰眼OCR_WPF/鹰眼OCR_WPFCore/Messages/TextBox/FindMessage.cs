using CommunityToolkit.Mvvm.Messaging.Messages;
using 鹰眼OCR_WPFCore.Messages.TextBox.MessageParam;

namespace 鹰眼OCR_WPFCore.Messages.TextBox
{
    /// <summary>
    /// 查找消息
    /// </summary>
    internal class FindMessage : ValueChangedMessage<FindParam>
    {
        public FindMessage(FindParam value) : base(value)
        {

        }
    }
}
