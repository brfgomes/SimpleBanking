using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Services;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Transactions")]

    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IServicesFactory _servicesFactory;

        public TransactionController(ILogger<TransactionController> logger, IRepositoryFactory repositoryFactory, IServicesFactory servicesFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _servicesFactory = servicesFactory;
        }

        [HttpPost("")]
        public ActionResult CreateTransaction([FromBody] CreateTransactionRequest transaction)
        {
            var useCaseTransaction = new TransactionUseCase(_repositoryFactory, _servicesFactory);
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