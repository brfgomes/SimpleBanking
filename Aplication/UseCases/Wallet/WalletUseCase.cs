using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Response.User;

namespace SimpleBanking.Aplication
{
    public class WalletUseCase : IWallet
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public WalletUseCase(IRepositoryFactory repositoryFactory)
        {
            _walletRepository = repositoryFactory.CreateWalletRepository();
            _userRepository = repositoryFactory.CreateUserRepository();
        }

        public GenericResponse GetAll()
        {
            var walletsList = _walletRepository.GetAllWallets();
            List<ListWalletResponse> list = new List<ListWalletResponse>();

            foreach (var wallet in walletsList)
            {
                list.Add(new ListWalletResponse(
                    wallet.Id,
                    _userRepository.GetUserById(wallet.Id.ToString()).Name,
                    wallet.Balance,
                    wallet.LastTransactionDate.ToString()
                ));
            }

            return new GenericResponse(true, "OK", list);
        }
    }
}