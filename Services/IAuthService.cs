using FutureOfWorkAPI.Models;

namespace FutureOfWorkAPI.Services;

public interface IAuthService
{
    User? ValidateUser(string username, string password);
    string GenerateJwtToken(User user);
}
