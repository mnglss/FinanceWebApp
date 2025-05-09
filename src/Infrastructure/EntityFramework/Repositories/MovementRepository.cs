using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;

namespace Infrastructure.EntityFramework.Repositories
{
    public class MovementRepository(AppDbContext context) : Repository<Movement>(context), IMovementRepository
    {
    }
}
