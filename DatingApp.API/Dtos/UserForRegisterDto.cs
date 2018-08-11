//Note: Dtos are use to map domain models to simpler obj
namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}