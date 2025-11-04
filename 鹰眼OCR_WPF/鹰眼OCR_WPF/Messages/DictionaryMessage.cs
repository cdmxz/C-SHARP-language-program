using CommunityToolkit.Mvvm.Messaging.Messages;

namespace 鹰眼OCR_WPF.Messages
{
    public class DictionaryMessage(Dictionary<string, object> value) : ValueChangedMessage<Dictionary<string, object>>(value)
    {
    }
}
