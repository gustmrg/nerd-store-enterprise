using NSE.WebApp.MVC.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddIdentityConfiguration();
builder.Services.AddMvcConfiguration(configuration);
builder.Services.AddServices(configuration);

var app = builder.Build();

app.UseMvcConfiguration(app.Environment);

app.Run();