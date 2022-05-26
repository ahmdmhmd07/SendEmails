using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using SendEmails.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SendEmails.Services
{
    public class MailingService : IMailingService
    {

        private readonly MailSettings _mailSettings;

        public MailingService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {





        var email = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Email), /*هعرفه مين السيندر*/
            Subject = subject /*هعرفه السابجيكت الللى جايلى فى البراميتر*/
        };







            email.To.Add(MailboxAddress.Parse(mailTo));/*عرفته الميل تو وهاخد الميل تو اللى جايلى بردو فى البراميترز*/

            var builder = new BodyBuilder();/*البلدر دا اللى بيكون فيه الاتاتش والبادى بتاع الايميل نفسه*/



            /*الاند يوزر ممكن يكون باعت اتاتشمين او ممكن يكون مش باعت */
            if (attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in attachments)
                {
                    if (file.Length > 0)
                    {
                        using var ms = new MemoryStream();/*لو الفايل اكبر من صفر ابتدى اقرا الفايل وضيفه فى الاتتاتشمينتس اللى جوا البلدر*/
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }




            builder.HtmlBody = body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));



            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);



        }
    }
}
