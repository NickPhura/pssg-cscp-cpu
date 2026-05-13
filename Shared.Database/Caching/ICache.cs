namespace Database;

public interface ICache
{
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan expiration);
}
