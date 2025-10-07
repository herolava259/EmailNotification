using Common.Domain.Generic.DDD;

namespace Common.DDD.Support.CRUD;


public abstract record QueryEntityBase<TEntity>
    where TEntity: DomainEntity
{ }


public abstract record CommandEntityBase<TEntity>
    where TEntity: DomainEntity
{ }

public record UpdateEntityModel<TEntity> : CommandEntityBase<TEntity>
    where TEntity: DomainEntity
{

    public string EntityId { get; set; } = String.Empty;
    public IReadOnlyDictionary<string, object> ModifyParams { get => _params;  }

    private readonly Dictionary<string, object> _params = new Dictionary<string, object>();


    public void Modify(string propertyName, object value)
        { _params[propertyName] = value; }


}


public interface IcrudRepository<TEntity>
    where TEntity : DomainEntity
{
    ValueTask<string?> CreateAsync(TEntity entity);

    Task<TEntity> FindByIdAsync(string id);

    Task<bool> ExistAsync(string id);

    Task<bool> ExistAsync(TEntity entity);

    Task<bool> RemoveAsync(TEntity entity);

    Task<bool> RemoveAsync(string id);

    Task<IEnumerable<TEntity>> QueryAsync<TEntityQuery>(TEntityQuery entityQuery)
        where TEntityQuery: QueryEntityBase<TEntity>;

    Task<(bool, TEntity)> UpdateAsync(TEntity entity);


    Task<(bool, TEntity)> PartialUpdateAsync(TEntity entity, params string[] modifiedProperties);

    Task<(bool, TEntity)> PartialUpdateAsync(UpdateEntityModel<TEntity> updateModel);
}
