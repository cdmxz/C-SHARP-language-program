using CommunityToolkit.Mvvm.Messaging.Messages;

namespace 鹰眼OCR_WPFCore.Messages
{
    public class DictionaryMessage : ValueChangedMessage<Dictionary<string, object>>
    {
        public DictionaryMessage(Dictionary<string, object> value) : base(value)
        {
        }
    }
}
