using CommunityToolkit.Mvvm.Messaging.Messages;
using 鹰眼OCR_Common.Messages.MessageParam;

namespace 鹰眼OCR_Common.Messages
{
    /// <summary>
    /// 截图消息
    /// </summary>
    public class ScreenshotMessage : ValueChangedMessage<ScreenshotParam>
    {
        public ScreenshotMessage(ScreenshotParam value) : base(value)
        {
        }
    }
}
