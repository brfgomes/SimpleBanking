namespace SimpleBanking.Aplication.Factories;

public interface IRepositoryFactory
{
    ITransactionRepository CreateTransactionRepository();
    IUserRepository CreateUserRepository();
    IWalletRepository CreateWalletRepository();
}