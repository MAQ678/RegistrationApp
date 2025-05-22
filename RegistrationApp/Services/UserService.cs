using ErrorOr;
using RegistrationApp.Controllers;
using RegistrationApp.Data;
using RegistrationApp.Errors;
using RegistrationApp.Models;

namespace RegistrationApp.Services
{
    public class UserService(ILogger<UserService> logger, AppDbContext context) : IUserService
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
                return Result.Success;
            }
            catch(Exception ex)
            {
                // Log the exception (ex) here if needed
                logger.LogError(ex, "An error occurred while registering the user.");
                return UserErrors.Unexpected;
            }
        }
    }
}
