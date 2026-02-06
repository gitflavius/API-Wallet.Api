using wallet.Api.Interfaces;
using Wallet.Api.Entities;
using wallet.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace wallet.Api.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly AppDbContext _context;

    public WalletRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Wallet.Api.Entities.Wallet?> GetByIdAsync(Guid id)
    {
        return await _context.Wallets.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<Wallet.Api.Entities.Wallet> CreateAsync(Wallet.Api.Entities.Wallet wallet)
    {
        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task UpdateAsync(Wallet.Api.Entities.Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }
}
