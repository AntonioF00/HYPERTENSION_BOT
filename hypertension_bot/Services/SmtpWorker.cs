using hypertension_bot.Settings;
using System.Net.Http;
using System.Net.Mail;

namespace hypertension_bot.Services
{
    internal class SmtpWorker
    {
        private bool res = true;
        public SmtpWorker() { }

        public bool Run()
        {
            try
            {
                SmtpClient mySmtpClient = new SmtpClient();
                // set smtp-client with basicAuthentication
                mySmtpClient.UseDefaultCredentials = true;
                mySmtpClient.Host = "localhost";
                mySmtpClient.Port = 587;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                System.Net.NetworkCredential(Setting.Istance.Configuration.Username, Setting.Istance.Configuration.Pwd);
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Credentials = basicAuthenticationInfo;
                // add from,to mailaddresses
                MailAddress from = new MailAddress(Setting.Istance.Configuration.Username, Setting.Istance.Configuration.NickName);
                MailAddress to = new MailAddress(Setting.Istance.Configuration.Recipient, Setting.Istance.Configuration.RecipientUsername);
                MailMessage myMail = new MailMessage(from, to);
                // add ReplyTo
                MailAddress replyTo = new MailAddress(Setting.Istance.Configuration.Recipient);
                myMail.ReplyToList.Add(replyTo);
                // set subject and encoding
                myMail.Subject = Setting.Istance.Configuration.Subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                // set body-message and encoding
                myMail.Body = Setting.Istance.Configuration.Body;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                // text or html
                myMail.IsBodyHtml = true;
                //Attachments 
                //myMail.Attachments.Add(new Attachment(@Setting.Istance.Configuration.Attachments));
                //invio la mail
                mySmtpClient.Send(myMail);
                return res;
            }
            catch(Exception ex){
                res = false;
                return res;
            }
        }
    }
}
