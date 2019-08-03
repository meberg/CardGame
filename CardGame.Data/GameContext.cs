using Microsoft.EntityFrameworkCore;
using CardGame.ClassLibrary;

namespace CardGame.Data
{
    public class GameContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserScore> UserScores { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = CardGame");
        }
    }
}
