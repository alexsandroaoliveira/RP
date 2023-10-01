using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Services;
using RapidPay.Modules.PaymentFees.Services;
using UFE;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<ICurrentDateProvider, CurrentDateProvider>();
// Adding Singletron for Universal Fees Exchange
builder.Services.AddSingleton<IUFEClient, UFESimulator>();

builder.Services.AddSingleton<RapidPayRepository>();

// CardManagement
builder.Services.AddScoped<ICardNumberServices, CardNumberServices>();
builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();

// PaymentFees
builder.Services.AddScoped<IPaymentFeesServices, PaymentFeesServices>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
