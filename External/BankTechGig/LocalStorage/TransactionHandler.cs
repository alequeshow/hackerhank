public class TransactionHandler() : LocalKeyValueStorage, ITransactionHandler
{
    private List<Transaction>? _transactions;

    private string TransactionId = "--";

    public async Task BeginAsync(string? transactionId = null)
    {
        TransactionId = transactionId ?? Guid.NewGuid().ToString()[..6];
        Console.WriteLine($"-- {TransactionId} - Starting Transaction");
        _transactions = [];
        await Task.CompletedTask;
    }

    public async Task CommitAsync()
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        foreach(var transaction in _transactions)
        {
            Console.WriteLine($"-- {TransactionId} - Executing {transaction.Type} for {transaction.Key}");
            await transaction.Execute();
        }
        Console.WriteLine($"-- {TransactionId} - Transaction Successfully Committed");
    }

    public async Task RollbackAsync()
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        Console.WriteLine($"-- {TransactionId} - Rollbacking Transaction - No changes persisted");
        _transactions.Clear();
        await Task.CompletedTask;
    }

    public new async Task InsertAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(item);

        var existingItem = await HasItemAsync(key);

        if(existingItem)
            throw new InvalidOperationException($"The key '{key}' already exists.");
        
        _transactions.Add(new Transaction(TransactionType.Insert, key, async () => await base.InsertAsync(key, item)));
    }

    public new async Task UpdateAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(item);

        var existingItem = await HasItemAsync(key);

        if(!existingItem)
            throw new KeyNotFoundException($"Key '{key}' not found.");

        _transactions.Add(new Transaction(TransactionType.Update, key, async () => await base.UpdateAsync(key, item)));
    }

    public new async Task UpsertAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(item);

        _transactions.Add(new Transaction(TransactionType.Upsert, key, async () => await base.UpsertAsync(key, item)));

        await Task.CompletedTask;
    }

    public new async Task DeleteAsync(string key)
    {
        ArgumentNullException.ThrowIfNull(_transactions);
        ArgumentException.ThrowIfNullOrEmpty(key);

        var existingItem = await HasItemAsync(key);

        if(!existingItem)
            throw new KeyNotFoundException($"Key '{key}' not found.");

        _transactions.Add(new Transaction(TransactionType.Delete, key, async () => await base.DeleteAsync(key)));
    }
}

public record Transaction(TransactionType Type, string Key, Func<Task> Execute);

public enum TransactionType
{
    Insert,
    Update,
    Upsert,    
    Delete,
}

