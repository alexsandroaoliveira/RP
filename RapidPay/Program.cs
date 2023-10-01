using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RapidPay.Modules;
using RapidPay.Modules.CardManagement.Data;
using RapidPay.Modules.CardManagement.Services;
using RapidPay.Modules.PaymentFees.Services;
using System.Text;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "RapidPay", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(bearerOptions =>
{
    var paramsValidation = bearerOptions.TokenValidationParameters;
    paramsValidation.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.SecretJwtKey!));
    paramsValidation.ValidAudience = tokenConfigurations.Audience;
    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
    // Valida a assinatura de um token recebido
    paramsValidation.ValidateIssuerSigningKey = true;
    // Verifica se um token recebido ainda é válido
    paramsValidation.ValidateLifetime = true;
    // Tempo de tolerância para a expiração de um token (utilizado
    // caso haja problemas de sincronismo de horário entre diferentes
    // computadores envolvidos no processo de comunicação)
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

// Ativa o uso do token como forma de autorizar o acesso
// a recursos deste projeto
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});

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
