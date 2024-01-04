using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 腾讯会议摸鱼助手.Utils
{
    internal class CustomMessageBox
    {
        public static DialogResult ShowError(string text)
        {
            return MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowTip(string text)
        {
            return MessageBox.Show(text, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
