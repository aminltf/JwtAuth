using Domain.Common.Base;

namespace Domain.Entities;

public class User : AuditableBase<int>
{
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public User() { } // For EF Core
}
