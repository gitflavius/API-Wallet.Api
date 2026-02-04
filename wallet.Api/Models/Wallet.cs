using System;

namespace Wallet.Api.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
