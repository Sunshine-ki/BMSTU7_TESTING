using System;
using System.Collections.Generic;
using System.Linq;

namespace project
{
	public class PostgreSQLRepositoryTask : IRepositoryTask
	{
		private ApplicationContext db;

		public PostgreSQLRepositoryTask()
		{
			this.db = new ApplicationContext();
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