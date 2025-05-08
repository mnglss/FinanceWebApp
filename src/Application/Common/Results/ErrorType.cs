namespace Application.Common.Results
{
    public static class ErrorType
    {
        public const string ValidationError = "ValidationError";
        public const string NotFoundError = "NotFoundError";
        public const string NotImplementedError = "NotImplementedError";
        public const string UnauthorizedError = "UnauthorizedError";
        public const string ForbiddenError = "ForbiddenError";
        public const string InternalServerError = "InternalServerError";
        public const string BadRequestError = "BadRequestError";
        public const string None = "None";
        public const string UnknownError = "UnknownError";
        public const string ConflictError = "ConflictError";
        public const string ServiceUnavailableError = "ServiceUnavailableError";
        public const string GatewayTimeoutError = "GatewayTimeoutError";
        public const string TooManyRequestsError = "TooManyRequestsError";
        public const string PreconditionFailedError = "PreconditionFailedError";
        public const string UnsupportedMediaTypeError = "UnsupportedMediaTypeError";
        public const string UnprocessableEntityError = "UnprocessableEntityError";
        public const string MethodNotAllowedError = "MethodNotAllowedError";
    }
}
