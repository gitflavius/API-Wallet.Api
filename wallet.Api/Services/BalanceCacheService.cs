using Microsoft.Extensions.Caching.Distributed;
using wallet.Api.Interfaces;

namespace wallet.Api.Services;

public class BalanceCacheService : IBalanceCacheService
{
    private readonly IDistributedCache _cache;

    public BalanceCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<decimal?> GetAsync(Guid walletId)
    {
        var value = await _cache.GetStringAsync($"wallet:balance:{walletId}");
        return value != null ? decimal.Parse(value) : null;
    }

    public async Task SetAsync(Guid walletId, decimal balance)
    {
        await _cache.SetStringAsync(
            $"wallet:balance:{walletId}",
            balance.ToString(),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
            });
    }

    public async Task InvalidateAsync(Guid walletId)
    {
        await _cache.RemoveAsync($"wallet:balance:{walletId}");
    }
}   