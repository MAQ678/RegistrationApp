using ErrorOr;
using RegistrationApp.Controllers;
using RegistrationApp.Data;
using RegistrationApp.Errors;
using RegistrationApp.Factory;
using RegistrationApp.Models;

namespace RegistrationApp.Services.Users
{
    public class UserService(ILogger<UserService> logger, AppDbContext context, EmailServiceFactory emailServiceFactory) : IUserService
    {
        public async Task<ErrorOr<Success>> RegisterUserAsync(User user)
        {
            try
            {
                bool userExists = context.Users.Any(u => u.Email == user.Email);
                if (userExists)
                {
                    return UserErrors.DuplicateEmail;
                }

                user.Id = Guid.NewGuid();
                context.Users.Add(user);
                await context.SaveChangesAsync();

                // Send a welcome email
                if(SendMail(user.Email, user.UserName))
                {
                    logger.LogInformation("Welcome email sent to {Email}", user.Email);
                }
                else
                {
                    logger.LogWarning("Failed to send welcome email to {Email}. Rmoving information from DB!", user.Email);
                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return UserErrors.EmailSendFailed;
                }

                return Result.Success;
            }
            catch(Exception ex)
            {
                // Log the exception (ex) here if needed
                logger.LogError(ex, "An error occurred while registering the user.");
                return UserErrors.Unexpected;
            }
        }

        private bool SendMail(string email, string userName)
        {
            var emailService = emailServiceFactory.GetEmailService();
            return emailService.SendEmail(email, "Welcome to Registration App", $"Hello <b>{userName}</b>,<br> Welcome to our app!");
        }
    }
}
