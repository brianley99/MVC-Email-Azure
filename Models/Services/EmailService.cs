﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace MVC_Email_Azure.Models.Services
{
    public class EmailService : IEmailSender
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailSender = _mailSettings.Email ?? Environment.GetEnvironmentVariable("Email");

            MimeMessage newEmail = new();

            newEmail.Sender = MailboxAddress.Parse(emailSender);

            foreach (var emailaddress in email.Split(";"))
            {
                newEmail.To.Add(MailboxAddress.Parse(emailaddress));
            }

            newEmail.Subject = subject;

            BodyBuilder emailBody = new();
            emailBody.HtmlBody = htmlMessage;

            newEmail.Body = emailBody.ToMessageBody();

            //Log in into smpt client
            using SmtpClient smtpClient = new();

            try
            {
<<<<<<< HEAD
                //var host = _mailSettings.MailHost ?? Environment.GetEnvironmentVariable("MailHost");
                //var port = _mailSettings.MailPort != 0 ? _mailSettings.MailPort : int.Parse(Environment.GetEnvironmentVariable("MailPort")!);
                //var password = _mailSettings.MailPassword ?? Environment.GetEnvironmentVariable("MailPassword");
                var host = "smtp.gmail.com";
                var port = int.Parse(Environment.GetEnvironmentVariable("MailPort")!);
                var password = "lqhnutzulyatxrgt";
=======
                var host = _mailSettings.MailHost ?? Environment.GetEnvironmentVariable("MailHost");
                var port = _mailSettings.MailPort != 0 ? _mailSettings.MailPort : int.Parse(Environment.GetEnvironmentVariable("MailPort")!);
                var password = _mailSettings.MailPassword ?? Environment.GetEnvironmentVariable("MailPassword");
>>>>>>> parent of 87c975d (update email connection 001)

                await smtpClient.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(emailSender, password);

                await smtpClient.SendAsync(newEmail);
                await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
    }
}
