using PDFtoImage;
using System;
using System.Drawing;
using System.IO;

namespace 鹰眼OCR.PDF
{
    class PdfHelper
    {
        public static bool IsPDFile(string fileName)
        {
            if (!File.Exists(fileName))
                return false;
            using StreamReader sr = new StreamReader(fileName);
            return sr.ReadLine().ToLower().IndexOf("pdf") != -1;
        }
    }
}