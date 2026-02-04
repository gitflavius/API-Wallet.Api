namespace wallet.Api.Interfaces;

public interface IBalanceCacheService
{
    Task<decimal?> GetAsync(Guid walletId);
    Task SetAsync(Guid walletId, decimal balance);
    Task InvalidateAsync(Guid walletId);
}
