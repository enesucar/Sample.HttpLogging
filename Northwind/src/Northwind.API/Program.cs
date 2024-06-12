using Northwind.API.Interfaces;
using Northwind.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomHttpLogging();
builder.Services.AddInMemoryHttpLoggingRepository();
builder.Services.AddEnableRequestBuffering();
builder.Services.AddRequestTime();

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEnableRequestBuffering();
app.UseRequestTime();
app.UseCustomHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
