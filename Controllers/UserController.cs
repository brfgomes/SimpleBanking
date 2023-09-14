using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;

            ExecuteDDL.Execute();
        }

        [HttpPost("Create")]
        public ActionResult Post([FromBody] CreateUserRequest user)
        {   
            var useCaseCreateUser = CreateUser.Execute(user);
            if(useCaseCreateUser == false)
                return BadRequest("Erro ao criar usuário!");

            return Ok("Usuário criado com sucesso!");
        }
    }
}