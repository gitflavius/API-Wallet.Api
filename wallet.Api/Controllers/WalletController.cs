using Microsoft.AspNetCore.Mvc;
using wallet.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly WalletService _walletService;

    public WalletController(WalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpGet("{id}/balance")]
    public async Task<IActionResult> GetBalance(Guid id)
    {
        var balance = await _walletService.GetBalanceAsync(id);
        return Ok(new { balance });
    }

    [HttpPost]
    public async Task<IActionResult> CreateWallet()
    {
        var wallet = await _walletService.CreateWalletAsync();
        return Ok(wallet);
    }

    [HttpPost("{id}/deposit")]
    public async Task<IActionResult> Deposit(Guid id, [FromBody] decimal amount)
    {
        await _walletService.DepositAsync(id, amount);
        return Ok();
    }   

    [HttpPost("{id}/withdraw")]
    public async Task<IActionResult> Withdraw(Guid id, [FromBody] decimal amount)
    {
        await _walletService.WithdrawAsync(id, amount);
        return Ok();
    }  

    [HttpPost("{id}/transfer")]
    public async Task<IActionResult> Transfer(Guid id, [FromBody] decimal amount)
    {
        await _walletService.TransferAsync(id, amount);
        return Ok();
    }
    
}
