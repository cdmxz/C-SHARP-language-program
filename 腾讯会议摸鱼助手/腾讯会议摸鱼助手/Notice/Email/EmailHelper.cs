using System.Net;
using System.Net.Http.Json;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace 腾讯会议摸鱼助手.Notice.Email
{
    internal class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="fromName">发件人名称</param>
        /// <param name="toEmail">收件人地址</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容（支持html格式）</param>
        public static void SendEmail(string fromName, string toEmail, string title, string body)
        {
            string fromEmail = "a2111385627@163.com";
            using MailMessage msg = new MailMessage();
            msg.To.Add(toEmail); // 添加收件人地址 
            // 发件人邮箱及名称
            msg.From = new MailAddress(fromEmail, fromName);
            msg.Subject = title;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;// 设置为HTML格式
            msg.Priority = MailPriority.High;// 优先级
            // 发送邮件
            using SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Host = "smtp.163.com";  // SMTP服务器地址 
            client.Port = 25;              // SMTP端口
            client.EnableSsl = true;       // 启用SSL加密
            // 设置邮箱账号和授权码
            client.Credentials = new NetworkCredential(fromEmail, "\u0050\u004a\u004e\u0050\u0054\u0054\u004f\u0045\u0049\u0057\u0048\u0057\u004d\u0051\u0046\u004d");
            client.Send(msg);
        }

        public static void SendEmail(string uuid, string email, string text)
        {
            string textBase64 = HttpUtility.UrlEncode(text);
            string url = "https://courier.toptopn.com/api/v1/cui/notify/push?";
            url += $"uuid={uuid}&title={textBase64}&url=url&emails={email}&text={textBase64}";
            using HttpClient httpClient = new HttpClient();
            EmailJson? json = httpClient.GetFromJsonAsync<EmailJson>(url).Result;
            if (json != null)
            {
                if (json.errno != 200)
                    throw new Exception("发送邮件失败！原因：" + json.errmsg);
            }
        }

        class EmailJson
        {
            public int errno { get; set; }
            public string? errmsg { get; set; }
        }
    }
}
