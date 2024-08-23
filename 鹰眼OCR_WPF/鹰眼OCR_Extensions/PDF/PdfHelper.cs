using System.IO;

namespace 鹰眼OCR_Extensions.PDF
{
    public class PdfHelper
    {
        public static bool IsPDFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }

            using StreamReader sr = new StreamReader(fileName);
            var line = sr.ReadLine();
            var result = line.Contains("pdf", StringComparison.OrdinalIgnoreCase);
            return result;
        }
    }
}