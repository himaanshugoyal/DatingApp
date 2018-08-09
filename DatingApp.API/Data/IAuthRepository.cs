using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    //By Convention start and interface with a Capital I
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}