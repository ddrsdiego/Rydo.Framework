using Rydo.Framework.Cache.Common;
using Rydo.Framework.Cache.Redis.Manager;
using ServiceStack.Redis;
using System;

namespace Rydo.Framework.Cache.Redis.Provider
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedisEndpoint _endPoint;

        public RedisCacheProvider()
        {
            var config = RedisConfigurationManager.Instance.Config;

            _endPoint = new RedisEndpoint(config.Host, config.Port, config.Password, config.DatabaseId);
        }

        public T Get<T>(string key)
        {
            T result = default(T);

            using (RedisClient redisClient = new RedisClient(_endPoint))
            {
                var wrapper = redisClient.As<T>();
                result = wrapper.GetValue(key);
            }

            return result;
        }

        public bool IsInCache(string key)
        {
            bool isInCache = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                isInCache = client.ContainsKey(key);
            }

            return isInCache;
        }

        public bool Remove(string key)
        {
            bool removed = false;

            using (RedisClient client = new RedisClient(_endPoint))
            {
                removed = client.Remove(key);
            }

            return removed;
        }

        public void Set<T>(string key, T value)
        {
            this.Set(key, value, TimeSpan.Zero);
        }

        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            using (RedisClient redisClient = new RedisClient(_endPoint))
            {
                redisClient.As<T>().SetValue(key, value, timeout);
            }
        }
    }
}
