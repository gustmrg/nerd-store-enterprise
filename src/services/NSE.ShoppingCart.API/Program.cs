using NSE.ShoppingCart.API.Configuration;
using NSE.WebAPI.Core.Identity;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddApiConfiguration(configuration);
builder.Services.AddAuthConfiguration(configuration);
builder.Services.AddServices();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseApiConfiguration();
app.UseSwaggerConfiguration();

app.Run();