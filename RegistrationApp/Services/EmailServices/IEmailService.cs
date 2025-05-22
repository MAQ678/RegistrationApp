namespace RegistrationApp.Services.EmailServices
{
    public interface IEmailService
    {
        bool SendEmail(string to, string subject, string body);
    }
}
