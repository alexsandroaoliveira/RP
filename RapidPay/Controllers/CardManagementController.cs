using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Modules.CardManagement.Services;

namespace RapidPay.Controllers;

[Authorize("Bearer")]
[ApiController]
[Route("[controller]")]
public class CardManagementController : ControllerBase
{
    private readonly ILogger<CardManagementController> _logger;
    private readonly ICardServices _cardServices;
    private readonly ITransactionServices _transactionServices;

    public CardManagementController(ILogger<CardManagementController> logger, ICardServices cardServices, ITransactionServices transactionServices)
    {
        _logger = logger;
        _cardServices = cardServices;
        _transactionServices = transactionServices;
    }

    [HttpGet("CreateCard")]
    public async Task<IActionResult> CreateCardAsync()
    {
        var card = await _cardServices.CreateCardAsync();

        _logger.LogInformation("Card created: {card}", card.Number);

        return Ok(card);
    }

    [HttpPost("Pay")]
    public async Task<IActionResult> PayAsync(string cardNumber, decimal amount)
    {
        var card = await _cardServices.GetCardAsync(cardNumber);

        if (card == null)
        {
            _logger.LogWarning("Post PayAsync - Card not found: {card}", cardNumber);
            return NotFound();
        }

        var payment = await _transactionServices.CreateTransactionAsync(card, amount);

        _logger.LogInformation("Post PayAsync - Payment created: {card}, {amount}", payment.Card.Number, payment.Amount);

        return Accepted();
    }

    [HttpGet("GetBalance")]
    public async Task<IActionResult> GetBalanceAsync(string cardNumber)
    {
        var card = await _cardServices.GetCardAsync(cardNumber);

        if (card == null)
        {
            _logger.LogWarning("Post PayAsync - Card not found: {card}", cardNumber);
            return NotFound();
        }

        var balance = await _transactionServices.GetBalanceAsync(card);

        _logger.LogInformation("Post GetBalanceAsync - Card: {card} Balance: {amount}", cardNumber, balance);

        return Ok(balance);
    }
}
