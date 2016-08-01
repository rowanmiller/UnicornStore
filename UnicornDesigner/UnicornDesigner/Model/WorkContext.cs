﻿using Microsoft.Data.Entity;

namespace UnicornDesigner
{
	public class WorkContext : DbContext
	{
		public DbSet<WorkOrder> WorkOrders { get; set; }
		public DbSet<Note> Notes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder builder)
		{
			var connectionString = @"Server=localhost;User ID=romiller;Database=Work";
			builder.UseNpgsql(connectionString);
		}
	} 
}

