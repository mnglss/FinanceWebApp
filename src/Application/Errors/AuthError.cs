using Application.Common.Results;

namespace Application.Errors
{
    public static class AuthError
    {
        public static Error ValidationFailure => Error.ValidationError("Validation failure");
        public static Error InvalidCredentials => Error.ValidationError("Invalid credentials provided.");
        public static Error UserAlreadyExists => Error.ValidationError("User with this email already exists.");
        public static Error UserNotFound => Error.NotFound("User not found.");
        public static Error InvalidPassword => Error.ValidationError("Invalid password provided.");
        public static Error UserNotActive => Error.Unauthorized("User is not active.");

        public static Error InvalidRequest(IEnumerable<string> errors) => Error.ValidationError(string.Join(" ",errors));
    }
}
