using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Transaction")]

    public class TransactionController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger, IUserRepository userRepository, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("Create")]
        public ActionResult CreateTransaction([FromBody] CreateTransactionRequest transaction)
        {
            var useCaseTransaction = new TransactionUseCase(_userRepository, _walletRepository, _transactionRepository);
            var result = useCaseTransaction.Create(transaction);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

    }
}