using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;
using SimpleBanking.Domain;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Wallet")]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IWallet _wallet;

        public WalletController(ILogger<WalletController> logger, IWallet wallet, IDDL executeDDl)
        {
            _logger = logger;
            _wallet = wallet;

            executeDDl.Execute();
        }

        [HttpGet("")]
        public ActionResult ListUsers()
        {
            var result = _wallet.GetAll();

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