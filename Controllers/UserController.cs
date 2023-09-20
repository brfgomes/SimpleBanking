using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IWalletRepository _walletRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IWalletRepository walletRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _walletRepository = walletRepository; 

            ExecuteDDL.Execute();
        }

        [HttpPost("")]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {   
            var useCaseUser = new UserUseCase(_userRepository, _walletRepository);
            var result = useCaseUser.Create(user);

            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("")]
        public ActionResult ListUsers()
        {
            var useCaseUser = new UserUseCase(_userRepository, _walletRepository);
            var result = useCaseUser.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("")]
        public ActionResult ChangeUser([FromBody] ChangeUserRequest user)
        {
            var useCaseUser = new UserUseCase(_userRepository, _walletRepository);
            var result = useCaseUser.Change(user);

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