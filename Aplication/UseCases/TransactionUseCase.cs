namespace SimpleBanking.Aplication
{
    public class TransactionUseCase : ITransaction
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionUseCase(IUserRepository userRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        public GenericResponse Create(CreateTransactionRequest transaction)
        {


            var sendler =  _userRepository.GetUserById(transaction.userSenderId);
            var receiver = _userRepository.GetUserById(transaction.userReceiverId);

            if (sendler.Type == Domain.EUserType.Seller)
            {
                return new GenericResponse(false, "Lojistas nao podem enviar transferencias!");
            }

            var sendlerWallet = _walletRepository.GetWalletByUserId(sendler.Id);
            var receiverWallet = _walletRepository.GetWalletByUserId(receiver.Id);

            if(transaction.value < 0)
            {
                return new GenericResponse(false, "Saldo inválido!");
            }

            if (sendlerWallet.Balance < transaction.value)
            {
                return new GenericResponse(false, "Saldo insuficiente!");
            }

            var newTransaction = new Domain.Transaction(transaction.value, sendler, receiver);
            _transactionRepository.Insert(newTransaction);

            //retirar da Wallet no banco o valor transferido
            var newBalanceSendler = sendlerWallet.Balance - transaction.value;
            var newBalanceReceiver = receiverWallet.Balance + transaction.value;

            _walletRepository.UpadateBalance(sendler.Id, newBalanceSendler);
            _walletRepository.UpadateBalance(receiver.Id, newBalanceReceiver);

            return new GenericResponse(true, "Tranferencia enviada com sucesso!");
        }
    }
}