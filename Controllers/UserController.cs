using Microsoft.AspNetCore.Mvc;
using SimpleBanking.Aplication;
using SimpleBanking.Aplication.Factories;

namespace SimpleBanking.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUser _user;

        public UserController(ILogger<UserController> logger, IUser user)
        {
            _logger = logger;
            _user = user;

            ExecuteDDL.Execute();
        }

        [HttpPost("")]
        public ActionResult CreateUser([FromBody] CreateUserRequest user)
        {   
            var result = _user.Create(user);

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
            var result = _user.GetAll();

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
            var result = _user.Change(user);

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