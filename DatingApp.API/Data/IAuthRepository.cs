using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    //By Convention start and interface with a Capital I
    //To tell our application about the IAuthRepository and AuthRepository and we take care of this in Startup.cs
    //Inside the start up class we are going to add this as a service.
    //When we do this, it will available for injection throughout the rest of our application
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}