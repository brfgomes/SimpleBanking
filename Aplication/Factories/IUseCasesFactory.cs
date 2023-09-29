namespace SimpleBanking.Aplication.Factories;

public interface IUseCasesFactory
{
    IUser CreateUserUseCase();
    ITransaction CreateTransactionUseCase();
    IWallet CreateWalletUseCase();
}