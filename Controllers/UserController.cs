using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepositoryFactory _repositoryFactory;

        public UserController(ILogger<UserController> logger,IRepositoryFactory repositoryFactory)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;

            ExecuteDDL.Execute();
        }

        [HttpPost("")]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {   
            var useCaseUser = new UserUseCase(_repositoryFactory);
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
            var useCaseUser = new UserUseCase(_repositoryFactory);
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
            var useCaseUser = new UserUseCase(_repositoryFactory);
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