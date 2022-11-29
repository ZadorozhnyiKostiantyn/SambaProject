

namespace SambaProject.Error
{
    public static class Errors
    {
        public static class User
        {
            public static ErrorOr.Error DuplicateUsername = ErrorOr.Error.Conflict(
                code: "User.DuplicateUsername",
                description: "User with given username already exists");
        }

        public static class Authentication
        {
            public static ErrorOr.Error InvalidCredentials = ErrorOr.Error.Conflict(
                code: "Auth.InvalidCred",
                description: "Invalid credentials");
        }
    }
}
