using MailDemo.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MailSender;
using MailSender.Entities;
using MailSender.Interfaces;
using MimeKit;
using Serilog;

namespace MailDemo.Services
{
    public class MailService : IClient, IMailService
    {
        private readonly MailSettings _settings;
        private readonly SmtpClient _client;

        public MailService(MailSettings settings)
        {
            _settings = settings;
            _client = new SmtpClient();
        }

        public async Task ConnectAsync()
        {
            await _client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect);
            _client.AuthenticationMechanisms.Remove("XOAUTH2");
            _client.Authenticate(_settings.From, _settings.Password);
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync(true);
            _client.Dispose();
        }

        public async Task<StatusSent> SendAsync(MailData mailData)
        {
            string fileName = mailData.Attachment.Path.Split('\\').Last() ?? mailData.Attachment.Name;
            try
            {
                await _client.SendAsync(CreateMessage(mailData));
                Log.Information($"Файл {fileName} успешно отправлен");
                return new StatusSent { Title = fileName, Status = Status.Success, Time = DateTime.Now };
            }
            catch (Exception e)
            {
                Log.Error($"Файл {fileName} не отправлен. Ошибка: {e.Message}");
                return new StatusSent { Title = fileName, Status = Status.Fail, Time = DateTime.Now };
            }
        }

        public MimeMessage CreateMessage(MailData mailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
            message.Sender = new MailboxAddress(_settings.DisplayName, _settings.From);

            message.To.Add(MailboxAddress.Parse(mailData.To));
            message.Subject = mailData.Subject;

            var body = new BodyBuilder();

            if (mailData.Attachment.Path != null)
                body.Attachments.Add(mailData.Attachment.Path);
            else
                body.Attachments.Add(mailData.Attachment.Name, mailData.Attachment.File);

            body.HtmlBody = mailData.Body;
            message.Body = body.ToMessageBody();
            return message;
        }
    }
}
