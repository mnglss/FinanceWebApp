
using Application.Common.Results;

namespace Application.Errors
{
    public static class MovementError
    {
        public static Error MovementNotFound => Error.NotFound("Movement not found.");
        public static Error MovementAlreadyExists => Error.BadRequest("Movement already exists.");
        public static Error InvalidRequest(IEnumerable<string> errors) => Error.ValidationError(string.Join(" ", errors));
        public static Error InternalServerError(string message) => Error.InternalServerError(message);
        public static Error InvalidDate(string message) => Error.ValidationError(message);
    }
}
