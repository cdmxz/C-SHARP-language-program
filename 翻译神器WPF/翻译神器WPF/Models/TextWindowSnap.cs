using System.Windows;

namespace 翻译神器WPF.Models
{
    public sealed class TextWindowSnap
    {
        public int DelayTime { get; init; }
        public double Top { get; init; }
        public double Left { get; init; }
        public string WindowLocation { get; init; } = "";
        public double Height { get; init; }
        public double Width { get; init; }
        public bool Topmost { get; init; }
    }
}
