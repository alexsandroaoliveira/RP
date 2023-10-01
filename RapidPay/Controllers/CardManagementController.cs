using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Modules.CardManagement.Models;
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
    public IActionResult CreateCard()
    {
        return Ok(_cardServices.CreateCard());
    }

    [HttpPost("Pay")]
    public IActionResult Pay(string cardNumber, decimal amount)
    {
        var card = _cardServices.GetCard(cardNumber);

        if (card == null)
            return NotFound();

        var _ = _transactionServices.CreateTransaction(card, amount);

        return Accepted();
    }

    [HttpGet("GetBalance")]
    public IActionResult GetBalance(string cardNumber)
    {
        var card = _cardServices.GetCard(cardNumber);

        if (card == null)
            return NotFound();
        
        var balance = _transactionServices.GetBalance(card);

        return Ok(balance);
    }
}
