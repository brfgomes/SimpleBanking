using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Database;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Services;
using SimpleBanking.Infra.Database;
using SimpleBanking.Infra.Database.Repositories;
using SimpleBanking.Infra.Factories;
using SimpleBanking.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDatabaseConnection, SQLiteAdapter>();
builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IServicesFactory, ServicesFactory>();
builder.Services.AddScoped<IUser, UserUseCase>();
builder.Services.AddScoped<ITransaction, TransactionUseCase>();
builder.Services.AddScoped<IWallet, WalletUseCase>();

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
