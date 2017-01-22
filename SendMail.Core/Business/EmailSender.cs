﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendMail.Core.Entity;
using System.Net.Mail;

namespace SendMail.Core.Business
{
    public class EmailSender
    {
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
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential(parameters.Email, parameters.Password);
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.Subject = parameters.EmailSubject;
            mail.IsBodyHtml = true;
            mail.Body = parameters.EmailText.Replace("@NOME", to.Name);

            //Setting From , To and CC
            mail.From = new MailAddress(parameters.Email, parameters.Name);
            mail.To.Add(new MailAddress(to.Email));

            smtpClient.Send(mail);
        }
    }
}