using Microsoft.EntityFrameworkCore;
using Wallet.Api.Entities;

namespace wallet.Api.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Wallet.Api.Entities.Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}