using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Response.Transaction;
using SimpleBanking.Aplication.Services;
using SimpleBanking.Infra.Services;

namespace SimpleBanking.Aplication
{
    public class TransactionUseCase : ITransaction
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public TransactionUseCase(IRepositoryFactory repositoryFactory, IServicesFactory servicesFactory)
        {
            _userRepository = repositoryFactory.CreateUserRepository();
            _walletRepository = repositoryFactory.CreateWalletRepository();
            _transactionRepository = repositoryFactory.CreateTransactionRepository();
            _authenticationService = servicesFactory.CreateAuthenticationService();
            _emailService = servicesFactory.CreateEmailService();
        }

        public GenericResponse Create(CreateTransactionRequest transaction)
        {
            var sendler =  _userRepository.GetUserById(transaction.userSenderId);
            var receiver = _userRepository.GetUserById(transaction.userReceiverId);

            var sendlerWallet = _walletRepository.GetWalletByUserId(sendler.Id);
            var receiverWallet = _walletRepository.GetWalletByUserId(receiver.Id);

            if (sendlerWallet.Balance < transaction.value)
            {
                return new GenericResponse(false, "Saldo insuficiente!");
            }

            #region Servico de authenticação
            Task<bool> authTransaction = _authenticationService.Authenticate();

            if (authTransaction.Result == false)
            {
                return new GenericResponse(false, "Não autorizado pela API de autenticacao");
            }
            #endregion
            #region Gravar transacao
            var newTransaction = new Domain.Transaction(transaction.value, sendler, receiver);
            _transactionRepository.Insert(newTransaction);
            #endregion
            #region Alterar saldo na carteira dos dois usuarios após a transferencia
            var newBalanceSendler = sendlerWallet.Balance - transaction.value;
            var newBalanceReceiver = receiverWallet.Balance + transaction.value;

            _walletRepository.UpdateBalance(sendler.Id, newBalanceSendler);
            _walletRepository.UpdateBalance(receiver.Id, newBalanceReceiver);
            #endregion
            #region Servico de envio de email de notificacao
            Task<GenericResponse> sendlerNotificationEmail = _emailService.SendEmail(sendler.Email.Address);
            Task<GenericResponse> receiverNotificationEmail = _emailService.SendEmail(receiver.Email.Address);

            if (sendlerNotificationEmail.Result.Success == false)
                return sendlerNotificationEmail.Result;

            if (receiverNotificationEmail.Result.Success == false)
                return receiverNotificationEmail.Result;

            #endregion

            return new GenericResponse(true, "Tranferencia enviada com sucesso!", new TransactionResponse(newTransaction.Id));
        }
    }
}