using System;

namespace Rydo.Framework.Cache.Common
{
    public interface ICacheProvider
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Set<T>(string key, T value, TimeSpan timeout);
        bool Remove(string key);
        bool IsInCache(string key);
    }
}
