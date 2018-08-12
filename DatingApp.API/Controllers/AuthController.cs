using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    //Note:ASP.net API is automatically going to infer, from a body, header, form

    //route is going to be /api/auth
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request
            // Note: ModelState validation is not needed if we have [ApiController Attribute at the top.]
            // if(!ModelState.IsValid)
            //     return BadRequest(ModelState);

            //convert the username to lower to bring in consistency.
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            //check if the user name is already taken 
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate,
            userForRegisterDto.Password);

            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //1. Check that that user name and password matches the credentials in the system
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            //Build Token
            //Token will contain 2 claims
            var claims = new[] {
                //Users Id
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                //Users Username
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //In order to make sure that the token is a valid token, the server needs to sign the token
            //Create Security Key
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSEttings:Token").Value));

            //use the key as a part of signing credentials and encrypting the key with a hashing algorithm
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //For creating token, we will first create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //claims is passed as the subject
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //expiry date
                SigningCredentials = creds //pass signing credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            //JwtSecurityTokenHAndler allows us to create the token based on the tokenDescriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //use the token variable to write the response back to the client.
            return Ok (new {
                token = tokenHandler.WriteToken(token)
            });

        }

    }
}