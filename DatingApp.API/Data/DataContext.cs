using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    //DbContext will a query to the database via entity framework.
    //DataContext is a derived class of DbContext
    public class DataContext : DbContext
    {
        /*$ dotnet ef migration add InitialCreate
         */
        //:base to chain the Constructor DataContext with DbContext
        public DataContext(DbContextOptions<DataContext> options): base (options){}

        //There is a convention to pluralise the entities
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        
    }
}