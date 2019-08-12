using Microsoft.EntityFrameworkCore;
using CardGame.ClassLibrary;
using Microsoft.Extensions.Logging; // Log sql-queries
using Microsoft.Extensions.Logging.Console; // Log sql-queries in console

namespace CardGame.Data
{
    public class GameContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserScore> UserScores { get; set; }

        // Log sql-queries to console
        public static readonly LoggerFactory MyConsoleLogger
            = new LoggerFactory(new[]
            {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name // only show sql-commands
                && level == LogLevel.Information, true) // level of detail (just basic information)
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                //.UseLoggerFactory(MyConsoleLogger) // Log sql-queries
                .UseSqlServer(
                "Server = (localdb)\\mssqllocaldb; Database = CardGame");
        }
    }
}
