using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendMail.Core.Entity;
using System.Net.Mail;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace SendMail.Core.Business
{
    public class EmailSender
    {
        public EmailResult Send(EmailData emailData)
        {
            var result = new EmailResult() { Name = emailData.ToName, Email = emailData.ToEmail };

            try
            {
                ExecuteSendMail(emailData);
                result.Success = true;
                result.Message = "Email enviado com sucesso";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        private void ExecuteSendMail(EmailData emailData)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(emailData.FromEmail, emailData.FromPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                using (var mail = new MailMessage())
                {
                    mail.Subject = Regex.Replace(emailData.EmailSubject, "@NOME", emailData.ToName, RegexOptions.IgnoreCase);
                    mail.IsBodyHtml = true;
                    mail.Body = Regex.Replace(emailData.EmailText, "@NOME", emailData.ToName, RegexOptions.IgnoreCase);

                    mail.From = new MailAddress(emailData.FromEmail, emailData.FromName);
                    mail.To.Add(new MailAddress(emailData.ToEmail));

                    smtpClient.Send(mail);
                }
            }
        }
    }
}
