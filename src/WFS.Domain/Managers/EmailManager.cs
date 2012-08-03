
using System;
using System.Net.Mail;

namespace WFS.Domain.Managers
{
    public class EmailManager
    {
        public bool SendEmail(string from, string to, string subject, string body)
        {
            try
            {
            //    var enabled = ConfigurationManager.AppSettings["EnableEmails"];
            //    if (enabled != null && !Convert.ToBoolean(enabled))
            //    {
            //        // we are deliberately disabling emails, so pretend it was sent
            //        return true;
            //    }

                using (MailMessage message = new MailMessage())
                {
                    if (!String.IsNullOrEmpty(from))
                    {
                        message.From = new MailAddress(from);
                    }

                    var recipients = to.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string recipent in recipients)
                    {
                        if (!string.IsNullOrEmpty(recipent))
                            message.To.Add(new MailAddress(recipent.Trim()));
                    }
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;


                    SmtpClient mailClient = new SmtpClient();
                    mailClient.Send(message);
                }
            }
            catch (Exception ex)
            {
            }

            return true;
        }

    }
}
