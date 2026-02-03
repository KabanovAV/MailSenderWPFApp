using MailSender.Entities;
using MimeKit;

namespace MailDemo.Interfaces
{
    public interface IMailService
    {
        MimeMessage CreateMessage(MailData mailData);
        Task<StatusSent> SendAsync(MailData mailData);
    }
}
