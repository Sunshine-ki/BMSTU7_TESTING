using System;
using Microsoft.EntityFrameworkCore;

namespace db
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Task> Tasks { get; set; }
		public DbSet<CompletedTask> CompletedTasks { get; set; }
		public DbSet<User> Users { get; set; }


		// public DbSet<UserTemporary> UsersTemporary { get; set; }

		// public ApplicationContext()
		// {
		// 	// Если БД не создана, то создаем.
		// 	Database.EnsureCreated();
		// }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db;Username=lis;Password=password");
		}

		// // Значение по умолчанию.
		// protected override void OnModelCreating(ModelBuilder modelBuilder)
		// {
		// 	modelBuilder.Entity<Task>()
		// 	.Property(b => b.AuthorId)
		// 	.HasDefaultValue(4);
		// }
	}
}