namespace Application.DTOs
{
    public record UserDto(int idUser, string firstName, string lastName, string email, DateOnly createdAt, DateOnly updatedAt, List<string> roles);
}
