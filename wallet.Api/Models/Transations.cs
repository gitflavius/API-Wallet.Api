public class Transaction
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty; // Credito / Debito
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
