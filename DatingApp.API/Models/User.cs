using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }    
        public string Country { get; set; }
        // Single user can have many photos
        public ICollection<Photo> Photos { get; set; }
        //Commands to Run after creating this model
        //dotnet ef migrations add AddedUserEntity 
        //dotnet ef database update
        
        // To list the migrations
        // dotnet ef migrations list 

        // To remove migrations
        // dotnet ef migrations remove

        // To drop database
        // dotnet ef database drop

        
    }
}