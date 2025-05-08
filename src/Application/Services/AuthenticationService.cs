using Application.Common.Results;
using Application.Errors;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using System.Security.Cryptography;

namespace Application.Services;

public class AuthenticationService(
    IUnitOfWork unitOfWork,
    IUserRepository userRepository,
    IValidator<RegisterRequest> registerUserValidator,
    IValidator<LoginRequest> loginUserValidator
) : IAuthenticationService
{
    private const int SaltSize = 16; // 128 / 8 // 16 bytes
    private const int HashSize = 32; // 256 / 8 // 32 bytes
    private const int IterationCount = 100000;
    private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

    public async Task<Result> LoginAsync(LoginRequest request)
    {
        var validationResult = await loginUserValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return Result.Failure(AuthError.InvalidRequest(errors));
        }

        var (email, password) = request;
        var user = await userRepository.GetUserByEmailAsync(email);
        if (user is null)
            return Result.Failure(AuthError.UserNotFound);
        if (!VerifyPassword(password, user.Password))
            return Result.Failure(AuthError.InvalidPassword);
        var token = "Token"; // await unitOfWork.TokenService.CreateTokenAsync(user);
        //return Result.Success(new AuthenticationResponse
        //{
        //    Token = token,
        //    User = new UserResponse
        //    {
        //        Id = user.Id,
        //        FirstName = user.FirstName,
        //        LastName = user.LastName,
        //        Email = user.Email,
        //        Roles = await userRepository.GetUserRoleByEmailAsync(user.Email),
        //    }
        //});
        return Result.Success(token);
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        var validationResult = await registerUserValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage);
            return Result.Failure(AuthError.InvalidRequest(errors));
        }

        if (await userRepository.UserExistsAsync(request.Email))
            return Result.Failure(AuthError.UserAlreadyExists);

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = HashPassword(request.Password),
        };
        await userRepository.AddAsync(user);
        await unitOfWork.CommitAsync();
        return Result.Success("User registered successfully!");
    }

    private string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: IterationCount,
            hashAlgorithm: HashAlgorithm,
            outputLength: HashSize);


        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    private bool VerifyPassword(string providedPassword, string hashedPassword)
    {
        var parts = hashedPassword.Split('-');
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromHexString(parts[1]);
        byte[] hash = Convert.FromHexString(parts[0]);

        byte[] inputHashed = Rfc2898DeriveBytes.Pbkdf2(
            password: providedPassword,
            salt: salt,
            iterations: IterationCount,
            hashAlgorithm: HashAlgorithm,
            outputLength: HashSize);

        // return hash.SequenceEqual(inputHashed);
        return CryptographicOperations.FixedTimeEquals(hash, inputHashed);
    }
}