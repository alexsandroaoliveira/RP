using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPay.Modules.CardManagement.Models;
using RapidPay.Modules.CardManagement.Services;

namespace RapidPay.Controllers;

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
    public Card CreateCard()
    {
        return _cardServices.CreateCard();
    }

    [HttpPost("Pay")]
    public void Pay(string cardNumber, decimal amount)
    {
        Card card = GetCard(cardNumber);

        var _ = _transactionServices.CreateTransaction(card, amount);
    }

    [HttpGet("GetBalance")]
    public decimal GetBalance(string cardNumber)
    {
        Card card = GetCard(cardNumber);
        
        return _transactionServices.GetBalance(card);
    }

    private Card GetCard(string cardNumber)
    {
        return _cardServices.GetCard(cardNumber) ?? throw new Exception("Invalid Card Number.");
    }
}
