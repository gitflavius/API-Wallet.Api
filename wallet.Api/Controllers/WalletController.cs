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
}