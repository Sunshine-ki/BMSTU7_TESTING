using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore.Design; //Add to your reference at top of solution.
using System.Linq;


namespace db
{
	public class MySQLApplicationContext : DbContext
	{
		public DbSet<Task> Tasks { get; set; }
		public DbSet<CompletedTask> CompletedTasks { get; set; }
		public DbSet<User> Users { get; set; }


		// public void ApplicationContext()
		// {
		// 	Database.EnsureCreated();
		// }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(
				"server=localhost;user=lis;password=password;database=ppo_lab;",
											new MySqlServerVersion(new Version(5, 7, 34))
			);
		}
	}
}