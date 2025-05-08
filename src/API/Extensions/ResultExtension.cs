using Application.Common.Results;

namespace API.Extensions
{
    public static class ResultExtension
    {
        public static IResult ToHttpResponse(this Result result)
        {
            if (result.IsSuccess)
                return Results.Ok(result);
            return MapErrorResponse(result.Error, result);
        }

        public static IResult ToHttpResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return Results.Ok(result);
            return MapErrorResponse(result.Error, result);
        }

        private static IResult MapErrorResponse(Error error, object result)
        {
            return error.Code switch
            {
                ErrorType.ValidationError => Results.BadRequest(result),
                ErrorType.NotFoundError => Results.NotFound(result),
                ErrorType.UnauthorizedError => Results.Unauthorized(),
                ErrorType.ForbiddenError => Results.Forbid(),
                _ => Results.Problem(detail: error.Message, statusCode: 500),
            };
        }
    }
}
