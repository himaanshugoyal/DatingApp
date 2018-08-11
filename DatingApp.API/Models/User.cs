namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        //Commands to Run after creating this model
        //dotnet ef migrations add AddedUserEntity 
        //dotnet ef database update
    }
}