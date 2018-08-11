using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    //Note:ASP.net API is automatically going to infer, from a body, header, form

    //route is going to be /api/auth
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            //convert the username to lower to bring in consistency.
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            //check if the user name is already taken 
            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate,
            userForRegisterDto.Password);

            return StatusCode(201);
            
        }
    }
}