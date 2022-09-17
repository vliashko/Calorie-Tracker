using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using UserMicroService.Contracts;

namespace UserMicroService.Services
{
    public class EmailService : IEmailService
    {
        private const string emailLogin = "caloriestracker@mail.ru";
        private const string password = "Fy35v5FfgiuUcXHc1fm9";

        public async Task SendEmailAsync(string email, string subject, string message, MemoryStream stream = null)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта Calories Tracker", emailLogin));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;

            BodyBuilder bb = new BodyBuilder();
            if(stream != null)
                bb.Attachments.Add("WeekReport.pdf", stream);
            else
                bb.TextBody = message;

            emailMessage.Body = bb.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync(emailLogin, password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
