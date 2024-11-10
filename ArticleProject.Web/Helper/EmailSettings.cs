using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace ArticleProject.Web.Helper
{

    public class EmailSettings : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("مقالتي: ", "j.ahmed.elsaied@gmail.com"));
            message.To.Add(new MailboxAddress("الكاتب: ", email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            message.Body = bodyBuilder.ToMessageBody();

            using (var emailSender = new SmtpClient())
            {
                await emailSender.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await emailSender.AuthenticateAsync("j.ahmed.elsaied@gmail.com", "nvomiolkynpstpcs");

                await emailSender.SendAsync(message);
                await emailSender.DisconnectAsync(true);
            }
        }
    }
}

