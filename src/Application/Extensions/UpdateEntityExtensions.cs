using Application.Models;
using Domain.Entities;

namespace Application.Extensions
{
    public static class UpdateEntityExtensions
    {
        public static User UpdateWithModel(this User entity, UserUpdateRequest model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.UpdatedAt = DateOnly.FromDateTime(DateTime.UtcNow);
            return entity;
        }
    }
}
