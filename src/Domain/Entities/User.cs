#nullable disable

using Domain.Common;

namespace Domain.Entities;

public class User(int userId, string username, string email, string password) : BaseEntity<int>()
{
    public int UserId { get; set; } = userId;
    public string Username { get; set; } = username;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
