namespace SproomInbox.API.Services
{
    public interface INullMailService
    {
        void SendEmail(string to, string emailMessage);
    }
}