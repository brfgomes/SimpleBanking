using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Infra.Database;
using SimpleBanking.Infra.Database.Repositories;

namespace SimpleBanking.Infra.Factories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IDatabaseConnection _databaseConnection;

    public RepositoryFactory(IDatabaseConnection databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public ITransactionRepository CreateTransactionRepository()
    {
        return new TransactionRepository(_databaseConnection);
    }

    public IUserRepository CreateUserRepository()
    {
        return new UserRepository(_databaseConnection);
    }

    public IWalletRepository CreateWalletRepository()
    {
        return new WalletRepository(_databaseConnection);
    }
}