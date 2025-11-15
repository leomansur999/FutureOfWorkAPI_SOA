namespace FutureOfWorkAPI.Models;

public class User
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // em trabalho real seria hash
    public string Role { get; set; } = "User"; // Admin ou User
}
