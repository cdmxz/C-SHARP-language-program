using Microsoft.Office.Interop.Word;
using QueryWordLib.WordHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 查询单词翻译GUI
{
    internal class RichTextBoxHelper
    {
        private const int WM_USER = 0x0400;
        private const int EM_GETPARAFORMAT = WM_USER + 61;
        private const int EM_SETPARAFORMAT = WM_USER + 71;
        private const long MAX_TAB_STOPS = 32;
        private const uint PFM_LINESPACING = 0x00000100;
        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

        /// <summary>
        /// 设置行间距
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="height">要指定的行高（磅）</param>
        public static void SetLineSpace(Control ctl, int height)
        {
            // 1磅=20缇
            int dyLineSpacing = height * 20;
            // dylinespace成员以 缇 的形式指定从一行到下一行的间距
            byte bLineSpacingRule = 3;
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = bLineSpacingRule;
            fmt.dyLineSpacing = dyLineSpacing;
            fmt.dwMask = PFM_LINESPACING;
            try
            {
                SendMessage(new HandleRef(ctl, ctl.Handle), EM_SETPARAFORMAT, bLineSpacingRule, ref fmt);
            }
            catch
            { }
        }

        /// <summary>
        /// 打开或创建word文档，全选word文档中的数据，然后全选richTextBox中的数据并复制到剪切板，然后粘贴到word文档中
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="fileName"></param>
        public static void SaveToWordDocument(RichTextBox richTextBox, string fileName)
        {
            // 删除同名文件
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            Microsoft.Office.Interop.Word.Application wordApp = new ApplicationClass();
            wordApp.Visible = true;
            object Nothing = Missing.Value;
            Document doc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            
            // 向word添加内容
            doc.ActiveWindow.Selection.WholeStory();// 全选Word文档中的内容
            richTextBox.SelectAll();// 全选Richtextbox中的内容
            Clipboard.SetData(DataFormats.Rtf, richTextBox.SelectedRtf);// 复制RTF数据到剪贴板
            doc.ActiveWindow.Selection.Paste();// 粘贴到Word文档

            // 设置页面边距 1厘米
            doc.PageSetup.TopMargin = 28.3f;
            doc.PageSetup.BottomMargin = 28.3f;
            doc.PageSetup.LeftMargin = 28.3f;
            doc.PageSetup.RightMargin = 28.3f;

            // 两栏内容
            doc.PageSetup.TextColumns.SetCount(2);
            // 设置文档的行间距
            doc.ActiveWindow.Selection.WholeStory();// 全选Word文档中的内容
            wordApp.Selection.ParagraphFormat.LineSpacing = 18f;
            wordApp.Selection.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
            wordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            //doc.Paragraphs.Last.Range.Font.Name = "微软雅黑";

            object format = WdSaveFormat.wdFormatDocumentDefault;
            object path = fileName; 
            // 将doc文档对象的内容保存为DOCX文档
            doc.SaveAs(ref path, ref format,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing);
        }
    }
}
