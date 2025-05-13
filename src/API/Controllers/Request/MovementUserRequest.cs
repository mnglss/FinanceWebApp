namespace API.Controllers.Request
{
    public class MovementUserRequest
    {
        public required MovementByUserIdDto MovementByUserIdDto { get; set; } = null!;
    }

    public class MovementByUserIdDto
    {
        public required int UserId { get; set; }
        public required int[] Year { get; set; }
        public required int[] Month { get; set; }

    }
}
