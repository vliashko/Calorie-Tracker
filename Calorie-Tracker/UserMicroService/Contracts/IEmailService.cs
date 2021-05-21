using System.Threading.Tasks;

namespace UserMicroService.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
