using Microsoft.Data.Entity;

namespace UnicornClicker.UWP.Models
{
    class UnicornClickerContext : DbContext
    {
        public DbSet<GameScore> GameScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=UnicornClicker.db");
        }
    }
}
