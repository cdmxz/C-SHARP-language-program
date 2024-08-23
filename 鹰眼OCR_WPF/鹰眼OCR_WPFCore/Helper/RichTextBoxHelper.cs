using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace 鹰眼OCR_WPFCore.Helper
{
    /// <summary>
    /// 实现RichTextBox的Document属性绑定
    /// </summary>
    public class RichTextBoxHelper : DependencyObject
    {
        public static readonly DependencyProperty DocumentTextProperty =
            DependencyProperty.RegisterAttached(
                "DocumentText",
                typeof(string),
                typeof(RichTextBoxHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDocumentTextChanged));

        public static string GetDocumentText(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentTextProperty);
        }

        public static void SetDocumentText(DependencyObject obj, string value)
        {
            obj.SetValue(DocumentTextProperty, value);
        }

        private static void OnDocumentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox richTextBox)
            {
                richTextBox.TextChanged -= RichTextBox_TextChanged;

                // 保存当前光标位置
                var caretPosition = richTextBox.CaretPosition;
                var offset = GetTextOffset(richTextBox.Document.ContentStart, caretPosition);

                var newText = (string)e.NewValue;
                var doc = richTextBox.Document;
                doc.Blocks.Clear();
                doc.Blocks.Add(new Paragraph(new Run(newText)));

                // 恢复光标位置
                var newCaretPosition = GetTextPointerAtOffset(richTextBox.Document.ContentStart, offset);
                richTextBox.CaretPosition = newCaretPosition;

                richTextBox.TextChanged += RichTextBox_TextChanged;
            }
        }

        private static void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
            {
                var text = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text.TrimEnd('\r', '\n');
                SetDocumentText(richTextBox, text);
            }
        }

        private static int GetTextOffset(TextPointer start, TextPointer position)
        {
            return new TextRange(start, position).Text.Length;
        }

        private static TextPointer GetTextPointerAtOffset(TextPointer start, int offset)
        {
            var navigator = start;
            while (navigator != null)
            {
                if (navigator.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    var textRun = navigator.GetTextInRun(LogicalDirection.Forward);
                    if (offset <= textRun.Length)
                    {
                        return navigator.GetPositionAtOffset(offset);
                    }
                    offset -= textRun.Length;
                }
                var nextContextPosition = navigator.GetNextContextPosition(LogicalDirection.Forward);
                if (nextContextPosition == null)
                {
                    break;
                }
                navigator = nextContextPosition;
            }
            return navigator;
        }
    }
}
