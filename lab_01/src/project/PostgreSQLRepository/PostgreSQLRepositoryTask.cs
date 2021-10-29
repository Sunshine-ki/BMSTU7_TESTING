using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace db
{
	public class PostgreSQLRepositoryTask : IRepositoryTask
	{
		private ApplicationContext db;

		public PostgreSQLRepositoryTask()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db;Username=lis;Password=password");
			this.db = new ApplicationContext(options.Options);
		}

		public PostgreSQLRepositoryTask(ApplicationContext db)
		{
			this.db = db;
		}

		public List<Task> GetTasks()
		{
			return db.Tasks.ToList();
		}

		public Task GetTask(int id)
		{
			return db.Tasks.Find(id);
		}
		public void Add(Task task)
		{
			db.Tasks.Add(task);
		}

		public void Delete(int id)
		{
			Task task = db.Tasks.Find(id);
			if (task != null)
				db.Tasks.Remove(task);

		}
		public void Save()
		{
			db.SaveChanges();
		}


		// TODO: Dispose вынести?
		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					db.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}