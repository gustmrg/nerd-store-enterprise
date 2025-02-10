using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Data;
using NSE.Catalog.API.Data.Repositories;
using NSE.Catalog.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<CatalogContext>();

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