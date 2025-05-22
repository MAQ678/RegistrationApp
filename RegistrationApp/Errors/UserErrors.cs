using ErrorOr;

namespace RegistrationApp.Errors
{
    public static class UserErrors
    {
        public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail", "Email already exists.");
        public static Error Unexpected => Error.Unexpected("User.Unexpected", "Unexpected error occurred.");

    }
}
