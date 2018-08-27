using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace SmartSociety.Models
{
    public class Mail
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        string smtpAddress = "smtp.gmail.com";
        int portNumber = 587;
        bool enableSSL = true;

        string emailFrom = "smartsocietyapp@gmail.com";
        string password = "smartypants";

        MailMessage mail = new MailMessage();

        public void sendMail()
        { 
            mail.From = new MailAddress(emailFrom);
            mail.To.Add(EmailTo);
            mail.Subject = Subject;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            // Can set to false, if you are sending pure text.

            //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
            //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                smtp.Send(mail);
            }
        }
    }
}