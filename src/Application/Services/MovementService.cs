using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class MovementService(
        IValidator<MovementRequest> movementRequestValidator,
        IMovementRepository movementRepository,
        IUnitOfWork unitOfWork
    ) : IMovementService
    {
        public async Task<Result<string>> CreateAsync(MovementRequest movementRequest)
        {
            try
            {
                var validationResult = await movementRequestValidator.ValidateAsync(movementRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Result.Failure<string>(UserError.InvalidRequest(errors));
                }

                var movement = new Movement { 
                    Id = 0, 
                    Year = movementRequest.year, 
                    Month = movementRequest.month, 
                    Date = DateOnly.Parse(movementRequest.date), 
                    Category = movementRequest.category, 
                    Description = movementRequest.description, 
                    Amount = movementRequest.amount, 
                    UserId = movementRequest.userId 
                };

                await movementRepository.AddAsync(movement);
                await unitOfWork.CommitAsync();
                return Result.Success("Movement created successfully");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(MovementError.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<List<Movement>>?> GetByUserIdAsync(MovementByUserIdRequest request)
        {
            var empty = Enumerable.Empty<Movement>();
            try
            {
                var movements = await movementRepository.GetByUserIdAsync(request.userId, request.year, request.month);
                if (movements == null || movements.Count == 0)
                    return Result.Success<List<Movement>>([.. empty]);
                return Result.Success(movements);
            }
            catch (Exception ex)
            {
                return Result.Failure<List<Movement>>(MovementError.InternalServerError(ex.Message));
            }
        }
    }
}
