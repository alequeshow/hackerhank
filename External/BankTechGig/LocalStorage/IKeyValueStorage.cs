public interface IKeyValueStorage
{
    Task InsertAsync<T>(string key, T item);
    Task UpdateAsync<T>(string key, T item);
    Task UpsertAsync<T>(string key, T item);
    Task<T> GetAsync<T>(string key);
    Task<bool> HasItemAsync(string key);
    Task DeleteAsync(string key);
}