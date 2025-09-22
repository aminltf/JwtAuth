using Domain.Common.Abstractions;

namespace Domain.Common.Base;

public abstract class EntityBase<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; } = default!;
}
