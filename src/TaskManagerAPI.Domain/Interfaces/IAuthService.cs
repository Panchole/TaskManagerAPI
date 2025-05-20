using TaskManagerAPI.Domain.Entities;

namespace TaskManagerAPI.Domain.Interfaces;

public interface IAuthService
{
    Task<(bool Success, string Token, User? User)> AuthenticateAsync(string username, string password);
    Task<string> GenerateJwtTokenAsync(User user);
    Task<bool> ValidatePasswordAsync(User user, string password);
    Task<string> HashPasswordAsync(string password);
}
