namespace Application.Common.Results
{
    public sealed record Error(string Code, string Message)
    {
        internal static Error None => new(ErrorType.None, string.Empty);
        internal static Error NotImplemented(string message) => new(ErrorType.NotImplementedError, message);
        internal static Error NotFound(string message) => new(ErrorType.NotFoundError, message);
        internal static Error ValidationError(string message) => new(ErrorType.ValidationError, message);
        internal static Error Unauthorized(string message) => new(ErrorType.UnauthorizedError, message);
        internal static Error Forbidden(string message) => new(ErrorType.ForbiddenError, message);
        internal static Error InternalServerError(string message) => new(ErrorType.InternalServerError, message);
        internal static Error BadRequest(string message) => new(ErrorType.BadRequestError, message);
    }
}
