using System;
using Microsoft.Extensions.Caching.Memory;

namespace challenge.investments.infrastructure.Helpers
{
    public static class MemoryCacheHelper
    {
        public static TEntity CreateMemoryCache<TEntity>
            (IMemoryCache memoryCache, string ulrClient, TEntity result) => memoryCache.GetOrCreate(ulrClient, e =>
            {
                e.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return result;
            });
    }
}
