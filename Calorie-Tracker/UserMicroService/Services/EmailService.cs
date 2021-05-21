using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using UserMicroService.Contracts;

namespace UserMicroService.Services
{
    public class EmailService : IEmailService
    {
        private const string emailLogin = "vladimir.lyashko@gmail.com";
        private const string password = "master12345A";

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Calories Tracker", emailLogin));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync(emailLogin, password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
