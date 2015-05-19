using System;
using Microsoft.Data.Entity;

namespace XamarinFormsTest
{
	public class GameContext : DbContext
	{
		public DbSet<Game> Games { get; set; }

		protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryStore ();
		}
	}
}

