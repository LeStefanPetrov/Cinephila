using System;
using System.Threading.Tasks;

namespace Cinephila.Domain.Redis
{
    public interface IRedisRepository
    {
        public Task<T> GetObjectAsync<T>(string key);

        public Task<bool> SetObjectAsync<T>(string key, T data, DateTime expiry);
    }
}
