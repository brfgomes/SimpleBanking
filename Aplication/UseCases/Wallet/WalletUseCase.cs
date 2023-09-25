using SimpleBanking.Aplication.Response.Transaction;
using SimpleBanking.Aplication.Response.User;
using SimpleBanking.Infra.Services;
using SimpleBanking.Infra.Services.Interfaces;

namespace SimpleBanking.Aplication
{
    public class WalletUseCase : IWallet
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public WalletUseCase(
            IWalletRepository walletRepository,
            IUserRepository userRepository)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
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