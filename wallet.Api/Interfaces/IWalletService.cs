using Wallet.Api.Entities;

namespace wallet.Api.Interfaces;

public interface IWalletService
{
    decimal Balance { get; }

    Task<decimal> GetBalanceAsync(Guid walletId);
}
