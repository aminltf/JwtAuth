using Domain.Common.Abstractions;

namespace Domain.Common.Base;

public abstract class AuditableBase<TKey> : EntityBase<TKey>, IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
