namespace Playground.JobFinder.Bases;

public interface IGeneralRepository
{
    Task<bool> SaveChangeAsync();
}


public enum ModifyAction: ushort
{
    Create = 0,
    Update = 1,
    Delete = 2,
}


public interface IGeneralDomainRepository<TEntity>: IGeneralRepository
    where TEntity: EntityBase
{
    Task<bool> SaveChangeAsync(TEntity entity, ModifyAction actionType);
}
