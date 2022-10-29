using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Fit5032Assignment.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(String toEmail, String subject, String contents, HttpPostedFileBase postfile)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("yuxingleod@icloud.com", "Cloud Patient Group");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            using (var memoryStream = new MemoryStream())
            {
                //fix the posted file
                postfile.InputStream.CopyTo(memoryStream);
                byte[] filebytes = memoryStream.ToArray();
                Attachment attachment = new Attachment();
                attachment.Content = Convert.ToBase64String(filebytes);
                attachment.Filename = postfile.FileName;
                msg.AddAttachment(attachment.Filename, attachment.Content);
            }

            var response = client.SendEmailAsync(msg);
        }
    }
}