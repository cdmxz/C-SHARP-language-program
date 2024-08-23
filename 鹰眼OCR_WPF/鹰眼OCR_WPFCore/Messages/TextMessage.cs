using CommunityToolkit.Mvvm.Messaging.Messages;

namespace 鹰眼OCR_WPFCore.Messages
{
    public class TextMessage : ValueChangedMessage<string>
    {
        public TextMessage(string value) : base(value)
        {
        }
    }
}
