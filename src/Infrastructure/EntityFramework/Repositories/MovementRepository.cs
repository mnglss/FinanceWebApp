﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework.Repositories
{
    public class MovementRepository(AppDbContext context) : Repository<Movement>(context), IMovementRepository
    {
        public async Task<List<Movement>?> GetByUserIdAsync(int userId, int[] year, int[] month)
        {
            return await context.Movements
                .Where(m =>
                    m.UserId == userId
                    && year.Contains(m.Year)
                    && month.Contains(m.Month)
                    )
                .OrderBy(m => m.Year)
                .ThenBy(m => m.Month)
                .ThenBy(m => m.Date)
                .ToListAsync();
        }

        public async Task<List<Category>> GetCategoryColorsAsync()
        {
            return await context.Categories.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
