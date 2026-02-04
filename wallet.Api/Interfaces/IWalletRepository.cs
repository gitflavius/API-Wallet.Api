namespace wallet.Api.Interfaces
{
    public interface IWalletRepository
    {
        Task<IWalletService?> GetByIdAsync(Guid walletId);
    }
}
