using System.Security.Cryptography;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class AuthenticationService(IUnitOfWork _unitOfWork, IUserRepository _userRepository) : IAuthenticationService
{
    private const int SaltSize = 16; // 128 / 8 // 16 bytes
    private const int HashSize = 32; // 256 / 8 // 32 bytes
    private const int IterationCount = 100000;
    private readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512; 

    public async Task<string> LoginAsync(LoginRequest request)
    {
        return await Task.FromResult("Not Implemented");
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var userExist = await _userRepository.GetUserByEmailAsync(request.Email);
        if (userExist is not null)
        {
            return "User already exists";
        }
        if (string.IsNullOrWhiteSpace(request.FirstName) || string.IsNullOrWhiteSpace(request.LastName) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        {
            return "All fields are required";
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = HashPassword(request.Password),
        };
        await _userRepository.AddAsync(user);
        await _unitOfWork.CommitAsync();
        return "User created successfully";
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