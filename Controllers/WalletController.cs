using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Wallet")]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletController(ILogger<WalletController> logger, IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _walletRepository = walletRepository; 

            ExecuteDDL.Execute();
        }

        [HttpGet("")]
        public ActionResult ListUsers()
        {
            var walletCaseUser = new WalletUseCase(_walletRepository, _userRepository);
            var result = walletCaseUser.GetAll();

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