using MailKit.Net.Smtp;
using MimeKit;
using MailKit;
using Microsoft.Extensions.Configuration;

namespace lr1.Services
{
    public class IEmailSender
    {
        private readonly IConfiguration _configuration;

        public IEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(
            string email, 
            string subject, 
            string message
            )
        {
            var mail = new MimeMessage();
            var settings = _configuration.GetSection("EmailSettings");

            mail.From.Add(new MailboxAddress(
                "Sender",
                settings["Email"]
                ));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = subject;
            mail.Body = new TextPart("plain") { Text = message };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                settings["Host"],
                int.Parse(settings["Port"]),
                MailKit.Security.SecureSocketOptions.StartTls
                );

            await smtp.AuthenticateAsync(
                settings["Email"],  
                settings["Password"]
                );
            await smtp.SendAsync(mail);
            await smtp.DisconnectAsync(true);
        }
    }
}