using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Infra.Database;
using SimpleBanking.Infra.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IDatabaseConnection, SQLiteAdapter>();
builder.Services.AddScoped<IDatabaseConnection, PostgresSQLAdapter>();

builder.Services.AddScoped<IRepositoryFactory, RepositoryFactory>();
builder.Services.AddScoped<IServicesFactory, ServicesFactory>();
builder.Services.AddScoped<IUser, UserUseCase>();
builder.Services.AddScoped<ITransaction, TransactionUseCase>();
builder.Services.AddScoped<IWallet, WalletUseCase>();
builder.Services.AddScoped<IDDL, DDL>();

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
