using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class DashBoardService(IMovementRepository movementRepository) : IDashBoardService
    {
        public async Task<Result<DashBoard>> GetDashBoardDataAsync(MovementByUserIdRequest request)
        {
            var dashboard = new DashBoard();
            var empty = Enumerable.Empty<Movement>();
            try
            {
                var movements = await movementRepository.GetByUserIdAsync(request.userId, request.year, request.month);
                if (movements!.Count != 0)
                {
                    dashboard.TotalIncome = movements.Where(m => m.Amount > 0).Sum(m => m.Amount);
                    dashboard.TotalOutcome = -movements.Where(m => m.Amount < 0).Sum(m => m.Amount);
                    var categoryList = movements
                        .Where(m => m.Amount < 0)
                        .Select(m => new { m.Category, m.Amount })
                        .GroupBy(m => m.Category)
                        .Select(r => new TotalCategory
                        {
                            Category = r.First().Category,
                            Amount = -r.Sum(s => s.Amount),
                            CategoryPercentage = Math.Round(100 * r.Sum(s => -s.Amount) / dashboard.TotalIncome, 2)
                        })
                        .OrderBy(m=> m.Category)
                        .ToList();
                    dashboard.TotalForCategory.AddRange(categoryList);
                    dashboard.Balance = dashboard.TotalIncome - dashboard.TotalOutcome;
                    var categoryPercentageTotal = categoryList.Sum(c => c.CategoryPercentage);
                    dashboard.BalancePercentage = 100 - categoryPercentageTotal;
                    dashboard.PieDataSource.Add(dashboard.BalancePercentage);
                    dashboard.PieDataSource.AddRange(categoryList.Select(p => p.CategoryPercentage));
                    dashboard.PieDataLabels.Add("Rimanenza");
                    dashboard.PieDataLabels.AddRange(categoryList.Select(p => p.Category));
                    dashboard.PieDataColors.Add("rgba(3, 130, 35, 0.5)");
                    dashboard.PieDataColors.AddRange(categoryList.Select(c => Category.Index[c.Category]).ToList());
                }
            }
            catch (Exception ex)
            {
                return Result.Failure<DashBoard>(MovementError.InternalServerError(ex.Message));
            }
            return Result.Success<DashBoard>(dashboard);
        }
    }
}
