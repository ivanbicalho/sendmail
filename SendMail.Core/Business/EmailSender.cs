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

namespace SendMail.Core.Business
{
    public class EmailSender
    {
        //public IEnumerable<EmailResult> Send(EmailData data)
        //{
        //    var result = new ConcurrentBag<EmailResult>();

        //    Parallel.ForEach(data.To, (to, loopState, index) =>
        //    {
        //        WaitNSecondsToSend(Convert.ToInt32(index));
        //        result.Add(SendMail(data.Parameters, to));
        //    });

        //    return result;
        //}

        //private void WaitNSecondsToSend(int seed)
        //{
        //    // Aguarda de 0 a 15 segundos para enviar a mensagem (usado para evitar erro no envio de e-mail por deficiência de vazão)
        //    System.Threading.Thread.Sleep(new Random(seed).Next(0, 15001));
        //}

        public IEnumerable<EmailResult> Send(EmailData data)
        {
            var result = new List<EmailResult>();

            foreach (var to in data.To)
            {
                result.Add(SendMail(data.Parameters, to));
            }

            return result;
        }

        private EmailResult SendMail(EmailParameters parameters, EmailEntity to)
        {
            var result = new EmailResult() { Name = to.Name, Email = to.Email };

            try
            {
                ExecuteSendMail(parameters, to);
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

        private void ExecuteSendMail(EmailParameters parameters, EmailEntity to)
        {
            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(parameters.Email, parameters.Password);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                using (var mail = new MailMessage())
                {
                    mail.Subject = parameters.EmailSubject.Replace("@NOME", to.Name);
                    mail.IsBodyHtml = true;
                    mail.Body = parameters.EmailText.Replace("@NOME", to.Name);

                    mail.From = new MailAddress(parameters.Email, parameters.Name);
                    mail.To.Add(new MailAddress(to.Email));

                    smtpClient.Send(mail);
                }
            }
        }
    }
}
