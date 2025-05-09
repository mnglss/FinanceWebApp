using Application.Common.Results;

namespace Application.Errors
{
    public static class UserError
    {
        public static Error UserNotFound => Error.NotFound("User not found.");

        public static Error UserEmailAlreadyExists => Error.BadRequest("Email already in use.");

        public static Error UserAlreadyHasRole => Error.BadRequest("User already has this role.");

        public static Error InternalServerError(string message) => Error.InternalServerError(message);

        public static Error InvalidRequest(IEnumerable<string> errors) => Error.ValidationError(string.Join(" ", errors));

    }
}
