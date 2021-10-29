using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace db
{
	public class PostgreSQLRepositoryCompletedTask : IRepositoryCompletedTask
	{
		private ApplicationContext db;

		public PostgreSQLRepositoryCompletedTask()
		{
			DbContextOptionsBuilder options = new DbContextOptionsBuilder();
			options.UseNpgsql("Host=localhost;Port=5432;Database=coursework_db;Username=lis;Password=password");
			this.db = new ApplicationContext(options.Options);
		}


		public PostgreSQLRepositoryCompletedTask(ApplicationContext db)
		{
			this.db = db;
		}

		public List<CompletedTask> GetCompletedTasks()
		{
			return db.CompletedTasks.ToList();
		}

		public CompletedTask GetCompletedTask(int id)
		{
			return db.CompletedTasks.Find(id);
		}
		public List<CompletedTask> GetCompletedTaskByUserId(int id)
		{
			return (from p in db.CompletedTasks
					where p.UserId == id
					select p).ToList();
		}
		public List<CompletedTask> GetCompletedTaskByTaskId(int id)
		{
			return (from p in db.CompletedTasks
					where p.TaskId == id
					select p).ToList();
		}

		public void Add(CompletedTask completedTask)
		{
			// if (db.Users.Find(completedTask.UserId) != null &&
			// db.Tasks.Find(completedTask.TaskId) != null)
			db.CompletedTasks.Add(completedTask);
		}

		public void Delete(int id)
		{
			CompletedTask completedTask = db.CompletedTasks.Find(id);
			if (completedTask != null)
				db.CompletedTasks.Remove(completedTask);

		}
		public void Save()
		{
			db.SaveChanges();
		}
	}
}