﻿namespace Application.Models
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string ConfirmPassword);
}
