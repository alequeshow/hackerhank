public interface ITransactionHandler : IKeyValueStorage
{
    Task BeginAsync(string? transactionId = null);

    Task CommitAsync();
    
    Task RollbackAsync();
}