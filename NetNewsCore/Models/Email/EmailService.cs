using Microsoft.Extensions.Options;
using NetNews.Models.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
//
using MailKit.Net.Smtp;
using MimeKit;
//
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NetNews.Models.Email
{
    public class EmailService
    {

        //Function to send email using  sendgrid
        public static bool SendEmail(string from_email, string to_email, string email_subject, string message_body, string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {
            AppFunctions functions = new AppFunctions();

            try
            {
                var apiKey = "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";
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(from_email, display_name);
                var subject = email_subject;
                var to = new EmailAddress(to_email, to_email.Split("@")[0]);
                var plainTextContent = functions.StripHTML(message_body);
                var htmlContent = message_body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = client.SendEmailAsync(msg);

                return true;
            }
            catch (Exception ex)
            {
                //TODO log error
                Console.WriteLine(ex);
                return false;
            }
        }

        //Function to send email using mailkit
        //public static bool SendEmail(string from_email, string to_email, string email_subject, string message_body, string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        //{
        //    try
        //    {
        //        MimeMessage message = new MimeMessage();

        //        MailboxAddress from = new MailboxAddress(display_name, from_email);
        //        message.From.Add(from);

        //        MailboxAddress to = new MailboxAddress(to_email.Split("@")[0], to_email);
        //        message.To.Add(to);

        //        message.Subject = email_subject;


        //        BodyBuilder bodyBuilder = new BodyBuilder();
        //        bodyBuilder.HtmlBody = message_body;
        //        bodyBuilder.TextBody = message_body;

        //        message.Body = bodyBuilder.ToMessageBody();

        //        SmtpClient client = new SmtpClient();
        //        client.Connect(smtp_host, smtp_port, false);
        //        client.Authenticate(smtp_email, smtp_pass);

        //        //send email message
        //        client.Send(message);
        //        client.Disconnect(true);
        //        client.Dispose();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO log error
        //        Console.WriteLine(ex);
        //        return false;
        //    }
        //}


        //Function to send email using email smtp
        /*
        public static bool SendSmtpEmail(string from_email, string to_email, string email_subject, string message_body, string smtp_email, string smtp_pass, string display_name, string smtp_host, int smtp_port)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtp_host);
            mail.From = new MailAddress(from_email, display_name);
            mail.To.Add(to_email);
            mail.Subject = email_subject;
            mail.Body = message_body;
            mail.IsBodyHtml = true;

            
            SmtpServer.Port = smtp_port;
            SmtpServer.Credentials = new NetworkCredential(smtp_email, smtp_pass);
            SmtpServer.EnableSsl = true;
           
            //SmtpServer.Timeout = 100000;
            //Test email

            //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //SmtpServer.EnableSsl = true;
            //SmtpServer.Host = "smtp.zoho.com";
            //SmtpServer.Port = 465;
            //SmtpServer.UseDefaultCredentials = false;
            //SmtpServer.Credentials = new NetworkCredential("emperorshat@zoho.com", "Hackasm@n1");

            try
            {
                SmtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                //TODO Log Error
                Console.WriteLine(ex);
                return false;
            }
        }
        */
    }
}
