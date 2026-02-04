using Microsoft.Extensions.Caching.Distributed;
using wallet.Api.Interfaces;
using Wallet.Api.Entities; // Added this usage

namespace wallet.Api.Services;

public class WalletService : IWalletService
{
    private readonly IDistributedCache _cacheService;
    private readonly IWalletRepository _repository;

    public WalletService(IDistributedCache cacheService, IWalletRepository repository)
    {
        _cacheService = cacheService;
        _repository = repository;
    }

    public decimal Balance => throw new NotImplementedException();

    public async Task<decimal> GetBalanceAsync(Guid walletId)
    {
        var cached = await _cacheService.GetStringAsync(walletId.ToString()); // Fixed GetAsync usage
        if (!string.IsNullOrEmpty(cached) && decimal.TryParse(cached, out var balance))
        {
             return balance;
        }

        var wallet = await _repository.GetByIdAsync( walletId);
        if (wallet == null) return 0; // Handle null wallet

        await _cacheService.SetStringAsync(walletId.ToString(), wallet.Balance.ToString());

        return wallet.Balance;
    }
}
