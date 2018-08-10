using System;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    /* Note:
    - This repositry is responsible for querying our database from entity framework
     Implement the interface and the methods implemented in the interface
        1. We need to inject our datacontext
        2. We inject the datacontext in the constructor function
        3. Use Initialize the field from parameter
        4. Add _ to the name as convention for private field
        5. Then we have access to our repository
     */
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }
        /* Note: Register
        By default the variable values are passed by values in function
        To pass the values by reference we use the 'out' keyword. Benefit of doing this, when we are going to update 
        the values of passwordHash in our function CreatePasswordHash, their values are going to be updated in the Register function
         */
        public async Task<User> Register(User user, string password)
        {
            byte [] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await  _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        /*
        If a function of a class is implementing IDisposable Interface then there is a dispose function present to dispose
        the resources by create using the function.
        To call the dispose function we make the call inside the using()
        So all the resources used inside the {} after the execution completes will be disposed

        Since ComputeHash function needs the values as byte [], we are converting Password to byte array using.
         System.Text.Encoding.UTF8.GetBytes(password)*/
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = hmac.Key; //this provides a randomly generated key
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}