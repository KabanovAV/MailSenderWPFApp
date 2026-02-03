namespace MailSender.Interfaces
{
    public interface IClient
    {
        Task ConnectAsync();
        Task DisconnectAsync();
    }
}
