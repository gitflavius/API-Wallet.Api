namespace wallet.Api.Interfaces
{
    public interface IWalletRepository
    {
        Task<Wallet.Api.Entities.Wallet?> GetByIdAsync(Guid walletId);
        Task<Wallet.Api.Entities.Wallet> CreateAsync(Wallet.Api.Entities.Wallet wallet);
        Task UpdateAsync(Wallet.Api.Entities.Wallet wallet);
    }
}
