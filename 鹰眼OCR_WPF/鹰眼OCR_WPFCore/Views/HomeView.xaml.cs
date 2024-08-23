using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Documents;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_WPFCore.Messages;
using 鹰眼OCR_WPFCore.Messages.TextBox;
using 鹰眼OCR_WPFCore.Messages.TextBox.MessageParam;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views
{
    /// <summary>
    /// HomeView.xaml 的交互逻辑
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView(HomeViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
            // 注册消息
            WeakReferenceMessenger.Default.Register<FindMessage, string>(this, MessageTokens.HomeView, Find);
            WeakReferenceMessenger.Default.Register<ReplaceMessage, string>(this, MessageTokens.HomeView, Replace);
        }


        // ****************************** RichTextBox查找文本 ****************************** 
        private int _findIndex;
        private static readonly char[] separator = [' ', '\t', '\r', '\n'];


        /// <summary>
        /// 查找文本
        /// </summary>
        private void Find(object recipient, FindMessage message)
        {
            if (Find(message.Value))
            {
                SendMessage("找到文本");
            }
            else
            {
                SendMessage("找不到文本");
            }
        }


        private bool Find(FindParam param)
        {
            if (param.IsPrevious)
            {
                return FindPrevious(param);
            }
            else
            {
                return FindNext(param);
            }
        }

        /// <summary>
        /// 从后往前查找文本
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private bool FindPrevious(FindParam param)
        {
            // 查找结果
            bool findResult = false;
            StringComparison comparison = param.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

            // 获取RichTextBox的文本内容
            var text = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Document.ContentEnd).Text;

            // 如果当前索引没有指向文本末尾，表示已经查找过了，把索引向前移动
            // 查找区间[0, _findIndex]
            // 向前查找索引
            if (_findIndex != text.Length - 1)
            {
                _findIndex = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Selection.Start).Text.Length - 1;
                if (_findIndex < 0)
                {
                    _findIndex = text.Length - 1;
                }
            }

            if (param.IsWholeWord)
            {
                // 全字符匹配
                var words = text.Substring(0, _findIndex + 1).Split(separator, StringSplitOptions.None);
                for (int i = words.Length - 1; i >= 0; i--)
                {
                    // 如果有当前单词与要查找的文本相等
                    // 表示[0, _findIndex]这个子串包含了要查找的文本
                    if (string.Equals(words[i], param.FindText, comparison))
                    {
                        // 查找当前单词在[0, _findIndex]这个子串中的索引位置
                        _findIndex = text.LastIndexOf(words[i], _findIndex, comparison);
                        if (_findIndex != -1)
                        {
                            // 选中找到的文本
                            var start = GetTextPointerAtOffset(LeftRichTextBox.Document.ContentStart, _findIndex);
                            var end = GetTextPointerAtOffset(start, param.FindText.Length);
                            LeftRichTextBox.Selection.Select(start, end);
                            LeftRichTextBox.Focus();
                            // 更新查找索引为选中文本的开始位置
                            _findIndex = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Selection.Start).Text.Length - LeftRichTextBox.Selection.Text.Length;
                            findResult = true;
                            break;
                        }
                    }
                    else
                    {
                        _findIndex -= words[i].Length;
                    }
                }
            }
            else
            {
                // 非全字符匹配
                _findIndex = text.LastIndexOf(param.FindText, _findIndex, comparison);

                if (_findIndex != -1)
                {
                    // 选中找到的文本
                    var start = GetTextPointerAtOffset(LeftRichTextBox.Document.ContentStart, _findIndex);
                    var end = GetTextPointerAtOffset(start, param.FindText.Length);
                    LeftRichTextBox.Selection.Select(start, end);
                    LeftRichTextBox.Focus();
                    findResult = true;
                }
            }
            // 匹配不成功，就把索引移动到文本末尾，
            // 下次查找从末尾开始
            if (!findResult)
            {
                _findIndex = text.Length - 1;
            }
            return findResult;
        }


        private bool FindNext(FindParam param)
        {
            // 查找结果
            bool findResult = false;
            StringComparison comparison = StringComparison.Ordinal;
            if (param.IgnoreCase)
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            // 查找区间[_findIndex, LeftRichTextBox.Document.ContentEnd)
            // 向后查找的索引
            if (_findIndex != 0)
            {
                _findIndex = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Selection.Start).Text.Length + 1;
                // 索引超出范围，就从头开始查找
                if (_findIndex >= new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Document.ContentEnd).Text.Length)
                {
                    _findIndex = 0;
                }
            }

            // 全字符匹配
            if (param.IsWholeWord)
            {
                // 查找区间[_findIndex, LeftRichTextBox.Document.ContentEnd)
                var text = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Document.ContentEnd).Text;
                var words = text.Substring(_findIndex).Split(separator, StringSplitOptions.None);
                for (int i = 0; i < words.Length; i++)
                {
                    // 如果当前单词与要查找的文本相等
                    if (string.Equals(words[i], param.FindText, comparison))
                    {
                        _findIndex = text.IndexOf(param.FindText, _findIndex, comparison);
                        // 选中找到的文本
                        var start = GetTextPointerAtOffset(LeftRichTextBox.Document.ContentStart, _findIndex);
                        var end = GetTextPointerAtOffset(start, param.FindText.Length);
                        LeftRichTextBox.Selection.Select(start, end);
                        LeftRichTextBox.Focus();
                        // 更新查找索引为选中文本的结束位置
                        _findIndex += param.FindText.Length;
                        findResult = true;
                        break;
                    }
                    else
                    {
                        _findIndex += words[i].Length;
                    }
                }
            }
            else
            {
                // 非全字符匹配
                var text = new TextRange(LeftRichTextBox.Document.ContentStart, LeftRichTextBox.Document.ContentEnd).Text;
                _findIndex = text.IndexOf(param.FindText, _findIndex, comparison);

                if (_findIndex != -1)
                {
                    var start = GetTextPointerAtOffset(LeftRichTextBox.Document.ContentStart, _findIndex);
                    var end = GetTextPointerAtOffset(start, param.FindText.Length);
                    LeftRichTextBox.Selection.Select(start, end);
                    LeftRichTextBox.Focus();
                    _findIndex += param.FindText.Length;
                    findResult = true;
                }
            }
            if (!findResult)
            {
                _findIndex = 0;
            }
            return findResult;
        }

        private static TextPointer GetTextPointerAtOffset(TextPointer start, int offset)
        {
            TextPointer navigator = start;
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

        /// <summary>
        /// 替换文本
        /// </summary>
        private void Replace(object recipient, ReplaceMessage message)
        {
            var param = message.Value;
            while (true)
            {
                if (Find(param))
                {
                    // LeftTextBox.SelectedText = message.Value.ReplaceText;
                    SendMessage("替换成功");
                    if (!param.IsReplaceAll)
                    {
                        break;
                    }
                }
                else
                {
                    SendMessage("找不到文本");
                    break;
                }
            }
        }


        /// <summary>
        /// 发送通知消息
        /// </summary>
        /// <param name="info"></param>
        private static void SendMessage(string info)
        {
            // 发送消息，给FindWindow
            WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.HomeView, info } }), MessageTokens.FindWindow);
        }

    }
}
