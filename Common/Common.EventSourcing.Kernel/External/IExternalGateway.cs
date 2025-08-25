namespace Common.EventSourcing.Kernel.External;
public interface IExternalGateway<TService>
    where TService : class
{
    public Task<TResult> QueryAsync<TQuery, TResult>(Func<TService, TQuery, Task<TResult>> queryTask, ExternalQueryWrapper<TQuery> queryWrapper)
        where TQuery: IExternalQuery;

    public ValueTask<bool> UpdateAsync<TCommand>(Func<TService, TCommand, ValueTask<bool>> queryTask, ExternalCommandWrapper<TCommand> commandWrapper)
        where TCommand: IExternalCommand;

    public Task NotifyAsync<TCommand>(Func<TService, TCommand, Task> queryTask, ExternalCommandWrapper<TCommand> commandWrapper)
        where TCommand : IExternalCommand;

}
