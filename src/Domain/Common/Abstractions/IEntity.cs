namespace Domain.Common.Abstractions;

public interface IEntity<TKey>
{
    TKey Id { get; }
}
