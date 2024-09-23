using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAccess.ServiceModel;

namespace DataAccess.Service
{
    public interface IEmailService
    {
        public void SendEmail(EmailServiceModel serviceMode);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailCredentials _emailCredentials;

        public EmailService(EmailCredentials emailCredentials) 
        {
            _emailCredentials = emailCredentials;
        }

        public void SendEmail(EmailServiceModel serviceModel)
        {
            try
            {
                // Set up the email message
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_emailCredentials.username);
                mail.To.Add(serviceModel.Email);
                mail.Subject = serviceModel.Subject;
                mail.Body = serviceModel.Body;

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient(_emailCredentials.client)
                {
                    Port = _emailCredentials.port, // Use port 587 for TLS
                    Credentials = new NetworkCredential(_emailCredentials.username, _emailCredentials.password),
                    EnableSsl = _emailCredentials.ssl,
                };

                // Send the email
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
