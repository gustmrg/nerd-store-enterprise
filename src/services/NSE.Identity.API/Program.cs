using NSE.Identity.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddApiConfiguration(configuration);
builder.Services.AddIdentityConfiguration(configuration);
builder.Services.AddMessageBusConfiguration(configuration);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();

app.Run();