namespace 翻译神器WPF.Common
{
    internal class VisibilityChangedMessage
    {
        public readonly bool NewValue;

        public VisibilityChangedMessage(bool newValue)
        {
            this.NewValue = newValue;
        }
    }
}