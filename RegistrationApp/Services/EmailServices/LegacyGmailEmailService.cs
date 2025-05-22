using System.Net.Mail;

namespace RegistrationApp.Services.EmailServices
{
    public class LegacyGmailEmailService(ILogger<LegacyGmailEmailService> logger) : BaseEmailService
    {
        private string smtpAddress = "smtp.gmail.com";
        private int portNumber = 587;
        private bool enableSSL = true;

        private string? senderEmailAddress = Environment.GetEnvironmentVariable("app_email", EnvironmentVariableTarget.User);
        private string? senderEmailPassword = Environment.GetEnvironmentVariable("app_password", EnvironmentVariableTarget.User); // TODO [Task]: Use secure storage for passwords.


        protected override bool Send(string to, string subject, string formattedBody)
        {
            logger.LogInformation($"[Gmail] To: {to} | Subject: {subject} | Body: {formattedBody}");
            logger.LogInformation($"[Gmail] Sender: {senderEmailAddress} | Password: {senderEmailPassword}");

            var message = GetMailMessage(to, subject, formattedBody);
            try
            {
                using SmtpClient smtpClient = new SmtpClient(smtpAddress, portNumber)
                {
                    Credentials = new System.Net.NetworkCredential(senderEmailAddress, senderEmailPassword),
                    EnableSsl = enableSSL
                };
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                logger.LogError($"[Gmail] Error sending email: {ex}");
                return false;
            }
            finally
            {
                message.Dispose();
            }
            return true;
        }

        private MailMessage GetMailMessage(string to, string subject, string formattedBody)
        {
            MailMessage mail = new()
            {
                From = new MailAddress(senderEmailAddress),
                Subject = subject,
                Body = formattedBody,
                IsBodyHtml = true
            };
            mail.To.Add(to);
            return mail;
        }
    }
}
