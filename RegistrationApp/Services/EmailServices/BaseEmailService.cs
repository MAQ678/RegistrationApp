

namespace RegistrationApp.Services.EmailServices
{
    public abstract class BaseEmailService : IEmailService
    {
        public bool SendEmail(string to, string subject, string body)
        {
            var formattedBody = FormatEmailBody(body);
            return Send(to, subject, formattedBody);
        }

        protected abstract bool Send(string to, string subject, string formattedBody);

        protected virtual string FormatEmailBody(string body)
        {
            return $"<html><body>{body} <br> <p>Regards, <br>Registration App team</p></body></html>";
        }
    }
}
