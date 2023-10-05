using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Aplication.Services;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Transaction")]

    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransaction _transaction;

        public TransactionController(ILogger<TransactionController> logger, ITransaction transaction, IDDL executeDDl)
        {
            _logger = logger;
            _transaction = transaction;
            
            executeDDl.Execute();
        }

        [HttpPost("")]
        public ActionResult CreateTransaction([FromBody] CreateTransactionRequest transaction)
        {
            var result = _transaction.Create(transaction);
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