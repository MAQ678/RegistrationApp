using ErrorOr;
using RegistrationApp.Models;

namespace RegistrationApp.Services.Users
{
    public interface IUserService
    {
        //Task<bool> UserExistsAsync(string email);
        //Task<bool> RegisterUserAsync(User user);
        Task<ErrorOr<Success>> RegisterUserAsync(User user);
    }
}
