using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Infra.Services.Interfaces;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Transaction")]

    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailService _emailService;

        public TransactionController(
            ILogger<TransactionController> logger,
            IUserRepository userRepository,
            IWalletRepository walletRepository,
            ITransactionRepository transactionRepository,
            IAuthenticationService authenticationService,
            IEmailService emailService
            )
        {
            _logger = logger;
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _authenticationService = authenticationService;
            _emailService = emailService;
        }

        [HttpPost("Create")]
        public ActionResult CreateTransaction([FromBody] CreateTransactionRequest transaction)
        {
            var useCaseTransaction = new TransactionUseCase(_userRepository, _walletRepository, _transactionRepository, _authenticationService, _emailService);
            var result = useCaseTransaction.Create(transaction);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

    }
}