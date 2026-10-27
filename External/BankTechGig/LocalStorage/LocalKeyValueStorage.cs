using System.Collections.Concurrent;

public class LocalKeyValueStorage : IKeyValueStorage
{
    private readonly ConcurrentDictionary<string, object> _storage = new();

    public async Task InsertAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(item);

        if(_storage.TryAdd(key, item))
        {
            await Task.CompletedTask;
            return;
        }

        throw new InvalidOperationException($"Key '{key}' already exists.");
    }

    public async Task UpdateAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(item);

        if(_storage.ContainsKey(key))
        {
            _storage[key] = item;
            await Task.CompletedTask;
            return;
        }

        throw new KeyNotFoundException($"Key '{key}' not found.");
    }

    public async Task UpsertAsync<T>(string key, T item)
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(item);

        _storage.AddOrUpdate(key,
            _ => item,
            (_, _) => item);

        await Task.CompletedTask;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if (_storage.TryGetValue(key, out var value) && value is T typedValue)
        {
            return await Task.FromResult(typedValue);
        }

        throw new KeyNotFoundException($"Key '{key}' not found.");
    }

    public async Task<bool> HasItemAsync(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        return await Task.FromResult(_storage.ContainsKey(key));;
    }

    public async Task DeleteAsync(string key)
    {
        ArgumentNullException.ThrowIfNull(key);

        if(_storage.TryRemove(key, out _))
        {
            await Task.CompletedTask;
            return;
        }
        
        throw new KeyNotFoundException($"Key '{key}' not found.");
    }    
}