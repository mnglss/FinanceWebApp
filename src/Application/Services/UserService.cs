using Application.Common.Results;
using Application.DTOs;
using Application.Errors;
using Application.Extensions;
using Application.Interfaces;
using Application.Models;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class UserService(IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IValidator<UserUpdateRequest> userUpdateValidator,
        IUserRoleRepository userRoleRepository
        ) : IUserService
    {
        public async Task<Result<string>> AssignRoleAsync(AssignRoleRequest roleRequest)
        {
            try
            {
                var userHasRole = await userRoleRepository.HasRoleAsync(roleRequest.userId, roleRequest.roleId);
                if (userHasRole)
                    return Result.Failure<string>(UserError.UserAlreadyHasRole);

                var result = await userRoleRepository.AddAsync(roleRequest.userId, roleRequest.roleId);
                    
                return result ? Result.Success("Role assigned successfully!") : Result.Failure<string>(UserError.InternalServerError("Failed to assign role"));
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(UserError.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<string>> DeleteAsync(int id)
        {
            try 
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user is null)
                    return Result.Failure<string>(UserError.UserNotFound);
                userRepository.Delete(user);
                await unitOfWork.CommitAsync();
                return Result.Success("User deleted successfully!");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(UserError.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<PagedResult<UserDto>>?> GetAllAsync(string? search, int pageNumber = 1, int pageSize = 10)
        {
            var pagedResult = new PagedResult<UserDto>();
            try
            {
                var allUsers = await userRepository.GetAllAsync();
                var totalUsers = allUsers.Count();
                var pagedItems = allUsers
                    .Where(x => string.IsNullOrWhiteSpace(search) || x.Email.ToLower().Contains(search.ToLower()))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(x => new UserDto(x.Id, x.FirstName, x.LastName, x.Email, x.CreatedAt, x.UpdatedAt, x.UserRoles != null ? x.UserRoles.Select(x => x.Role!.Name).ToList() : []))
                    .ToList();
                pagedResult = new PagedResult<UserDto>
                { 
                    Items = pagedItems,
                    TotalCount = totalUsers,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                return Result.Success(pagedResult);
            }
            catch (Exception ex)
            {
                return Result.Failure<PagedResult<UserDto>>(UserError.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(id);
                if (user is null)
                    return Result.Failure<UserDto>(UserError.UserNotFound);
                var userDto = new UserDto(user.Id, user.FirstName, user.LastName, user.Email, user.CreatedAt, user.UpdatedAt, user.UserRoles != null ? user.UserRoles.Select(x => x.Role!.Name).ToList() : []);
                return Result.Success(userDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<UserDto>(UserError.InternalServerError(ex.Message));
            }
        }

        public async Task<Result<string>> UpdateAsync(UserUpdateRequest userUpdateRequest)
        {
            try
            {
                var validationResult = await userUpdateValidator.ValidateAsync(userUpdateRequest);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Result.Failure<string>(UserError.InvalidRequest(errors));
                }
                var user = await userRepository.GetByIdAsync(userUpdateRequest.Id);
                if (user is null)
                    return Result.Failure<string>(UserError.UserNotFound);
                if (user.Email != userUpdateRequest.Email && await userRepository.UserExistsAsync(userUpdateRequest.Email))
                    return Result.Failure<string>(UserError.UserEmailAlreadyExists);
                user.UpdateWithModel(userUpdateRequest);
                userRepository.Update(user);
                await unitOfWork.CommitAsync();
                return Result.Success("User updated successfully!");
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(UserError.InternalServerError(ex.Message));
            }
        }
    }
}