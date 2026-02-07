namespace wallet.Api.Interfaces;

public interface IWalletService
{
    decimal Balance { get; }

    Task<decimal> GetBalanceAsync(Guid walletId);
    Task<Wallet.Api.Entities.Wallet> CreateWalletAsync();
    Task DepositAsync(Guid walletId, decimal amount);
    Task WithdrawAsync(Guid walletId, decimal amount);
    Task TransferAsync(Guid fromWalletId, decimal amount);
}
