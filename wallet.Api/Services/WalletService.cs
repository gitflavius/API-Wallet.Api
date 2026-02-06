using Microsoft.Extensions.Caching.Distributed;
using wallet.Api.Interfaces;


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
    public async Task<Wallet.Api.Entities.Wallet> CreateWalletAsync()
    {
        var wallet = new Wallet.Api.Entities.Wallet
        {
            Id = Guid.NewGuid(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow
        };

        var createdWallet = await _repository.CreateAsync(wallet);
        
        // Cache the initial balance
        await _cacheService.SetStringAsync(createdWallet.Id.ToString(), "0");

        return createdWallet;
    }

    public async Task DepositAsync(Guid walletId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive");
        }

        var wallet = await _repository.GetByIdAsync(walletId);
        if (wallet == null)
        {
            throw new InvalidOperationException("Wallet not found");
        }

        wallet.Balance += amount;
        await _repository.UpdateAsync(wallet);

        // Update cache
        await _cacheService.SetStringAsync(walletId.ToString(), wallet.Balance.ToString());
    }

    public async Task WithdrawAsync(Guid walletId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive");
        }

        var wallet = await _repository.GetByIdAsync(walletId);
        if (wallet == null)
        {
            throw new InvalidOperationException("Wallet not found");
        }

        if (wallet.Balance < amount)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        wallet.Balance -= amount;
        await _repository.UpdateAsync(wallet);

        // Update cache
        await _cacheService.SetStringAsync(walletId.ToString(), wallet.Balance.ToString());
    }

    public async Task TransferAsync(Guid fromWalletId, decimal amount)
    {
        // Note: This method signature only has fromWalletId. 
        // You may want to add a 'toWalletId' parameter to transfer between wallets.
        // For now, this will just withdraw from the wallet.
        await WithdrawAsync(fromWalletId, amount);
    }
}
