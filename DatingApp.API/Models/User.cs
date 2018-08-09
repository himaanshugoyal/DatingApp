namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; } 
        public int Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        //Commands to Run after creating this model
        //dotnet ef migrationsadd AddedUserEntity 
        //dotnet ef database update
    }
}