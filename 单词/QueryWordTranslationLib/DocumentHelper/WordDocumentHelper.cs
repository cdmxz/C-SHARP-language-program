using Microsoft.Office.Interop.Word;
using QueryWordLib.WordHelper;
using System.IO;
using System.Reflection;

namespace QueryWordLib.DocumentHelper
{
    /// <summary>
    /// 操作word文档
    /// </summary>
    public class WordDocumentHelper
    {
        ///// <summary>
        ///// 将单词信息导出到word文档 限制500个段落
        ///// 
        ///// </summary>
        ///// <param name="wordInfo"></param>
        ///// <param name="outPath"></param>
        //public static void ExportToWordDocument(WordInfo[] wordInfo, string outPath)
        //{
        //    // 删除同名文件
        //    if (File.Exists(outPath))
        //    {
        //        File.Delete(outPath);
        //    }
        //    Document doc = new Document();
        //    Section section = doc.AddSection();
        //    section.PageSetup.PageSize = PageSize.A4;
        //    // 设置页面边距 1厘米
        //    section.PageSetup.Margins.Top = 28.3F;
        //    section.PageSetup.Margins.Bottom = 28.3F;
        //    section.PageSetup.Margins.Left = 28.3F;
        //    section.PageSetup.Margins.Right = 28.3F;
        //    // 设置分栏
        //    section.AddColumn(150f, 10f);
        //    section.AddColumn(150f, 10f);
        //    section.MakeColumnsSameWidth();
        //    // 显示分割线
        //    // section.PageSetup.ColumnsLineBetween = true;

        //    // 创建段落样式
        //    ParagraphStyle style1 = new ParagraphStyle(doc);
        //    style1.Name = "style1";
        //    style1.CharacterFormat.FontName = "等线";
        //    style1.CharacterFormat.FontSize = 16F;
        //    ParagraphStyle style2 = new ParagraphStyle(doc);
        //    style2.Name = "style2";
        //    style2.CharacterFormat.FontName = "等线";
        //    style2.CharacterFormat.FontSize = 10.5F;
        //    doc.Styles.Add(style1);
        //    doc.Styles.Add(style2);

        //    Paragraph para;
        //    // 向word添加内容
        //    foreach (var item in wordInfo)
        //    {
        //        para = section.AddParagraph();
        //        // 设置段落中行距
        //        para.Format.LineSpacing = 18f;
        //        para.Format.LineSpacingRule = LineSpacingRule.Exactly;
        //        // 应用段落样式
        //        para.ApplyStyle("style1");
        //        // 单词 名称部分
        //        para.AppendText(item.Word);

        //        // 单词 音标和翻译部分
        //        // 避免音标为空而在word中增加一行空行
        //        string str = string.IsNullOrEmpty(item.Phonetic) ? "" : item.Phonetic + "\r\n";
        //        // wordDoc.Paragraphs.Last.Range.Text = str + item.Explanation + "\n\n";
        //        para = section.AddParagraph();
        //        // 设置段落中行距
        //        para.Format.LineSpacing = 18f;
        //        para.Format.LineSpacingRule = LineSpacingRule.Exactly;
        //        // 应用段落样式
        //        para.ApplyStyle("style2");
        //        para.AppendText(str + item.Explanation + "\r\n");
        //    }

        //    doc.SaveToFile(outPath, FileFormat.Docx2013);
        //    doc.Close();
        //}


        /// <summary>
        /// 将单词信息导出到word文档 
        /// 需要安装MSOffice
        /// 
        /// </summary>
        /// <param name="wordInfo"></param>
        /// <param name="outPath"></param>
        public static void ExportToWordDocument(WordInfo[] wordInfo, string outPath)
        {
            // 删除同名文件
            if (File.Exists(outPath))
            {
                File.Delete(outPath);
            }

            Document wordDoc;
            Application wordApp = new ApplicationClass();
            wordApp.Visible = true;
            object Nothing = Missing.Value;
            wordDoc = wordApp.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            // 设置页面边距 1厘米
            wordDoc.PageSetup.TopMargin = 28.3f;
            wordDoc.PageSetup.BottomMargin = 28.3f;
            wordDoc.PageSetup.LeftMargin = 28.3f;
            wordDoc.PageSetup.RightMargin = 28.3f;
            // 两栏内容
            wordDoc.PageSetup.TextColumns.SetCount(2);
            wordApp.Selection.ParagraphFormat.LineSpacing = 18f;// 设置文档的行间距
            wordApp.Selection.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
            wordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            wordDoc.Paragraphs.Last.Range.Font.Name = "等线";

            // 向word添加内容
            foreach (var item in wordInfo)
            {
                wordDoc.Paragraphs.Last.Range.Font.Size = 16f;
                wordDoc.Paragraphs.Last.Range.Text = item.Word + "\n";


                wordDoc.Paragraphs.Last.Range.Font.Size = 10.5f;
                string str = string.IsNullOrEmpty(item.Phonetic) ? "" : item.Phonetic + "\n";
                wordDoc.Paragraphs.Last.Range.Text = str + item.Explanation + "\n\n";
            }

            object format = WdSaveFormat.wdFormatDocumentDefault;
            object path = outPath;
            // 将wordDoc文档对象的内容保存为DOCX文档
            wordDoc.SaveAs(ref path, ref format,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing, ref Nothing, ref Nothing,
                ref Nothing, ref Nothing);
            // 关闭文档
            //wordDoc.Close(ref Nothing, ref Nothing, ref Nothing);
            //object saveOption = WdSaveOptions.wdSaveChanges;
            //wordApp.Application.Quit(ref saveOption, ref Nothing, ref Nothing);
        }
    }

}
