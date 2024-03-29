﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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

// Add services to the container

// Adding as singleton, Could be a static class as well
builder.Services.AddSingleton<ICurrentDateProvider, CurrentDateProvider>();

// Adding Singleton for Universal Fees Exchange
builder.Services.AddSingleton<IUFEClient, UFESimulator>();

// Adding Singleton for in-memory database
builder.Services.AddSingleton<RapidPayRepository>();

// CardManagement Module
builder.Services.AddScoped<ICardNumberServices, CardNumberServices>();
builder.Services.AddScoped<ICardServices, CardServices>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();

// PaymentFees Module
builder.Services.AddScoped<IPaymentFeesServices, PaymentFeesServices>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Adding swagger gen with Login in feature
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

// Getting token config from AppSettings
var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

// Adding Authentication Services to work with Bearer Jwt Token
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
    paramsValidation.ValidateIssuerSigningKey = true;
    paramsValidation.ValidateLifetime = true;
    paramsValidation.ClockSkew = TimeSpan.Zero;
});

// Adding Authorization Services to work with Bearer Jwt Token
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
        .RequireAuthenticatedUser().Build());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
