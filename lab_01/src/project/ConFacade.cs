using System;
using System.Collections.Generic;
using System.Linq;

using bl;

namespace db
{
	public class ConFacade : bl.IFacade
	{
		Converter converter;
		IRepositoryTask dbTask = new PostgreSQLRepositoryTask();
		IRepositoryUser dbUser = new PostgreSQLRepositoryUser();
		IRepositoryCompletedTask dbCompletedTask = new PostgreSQLRepositoryCompletedTask();

		// IRepositoryTask dbTask = new MySQLRepositoryTask();
		// IRepositoryUser dbUser = new MySQLRepositoryUser();
		// IRepositoryCompletedTask dbCompletedTask = new MySQLRepositoryCompletedTask();



		public ConFacade()
		{
			converter = new Converter();
			// IRepositoryTask dbTask = new PostgreSQLRepositoryTask();

		}

		public List<bl.Task> GetTasks()
		{
			List<db.Task> tasks = dbTask.GetTasks();
			List<bl.Task> result = new List<bl.Task>();

			foreach (db.Task elem in tasks)
				result.Add(converter.ConvertTaskToBL(elem));

			return result;
		}
		public List<bl.User> GetUsers()
		{
			List<db.User> users = dbUser.GetUsers();
			List<bl.User> result = new List<bl.User>();

			foreach (db.User elem in users)
				result.Add(converter.ConvertUserToBL(elem));

			return result;
		}
		public List<bl.CompletedTask> GetCompletedTasks()
		{
			List<db.CompletedTask> completedTask = dbCompletedTask.GetCompletedTasks();
			List<bl.CompletedTask> result = new List<bl.CompletedTask>();

			foreach (db.CompletedTask elem in completedTask)
				result.Add(converter.ConvertCompletedTaskToBL(elem));

			return result;
		}

		public int AddTask(bl.Task task)
		{
			db.Task taskDB = converter.ConvertTaskToBD(task);
			taskDB.Id = 0;
			dbTask.Add(taskDB);
			dbTask.Save();
			return 0;
		}

		public int AddUser(bl.User user)
		{
			db.User userDB = converter.ConvertUserToBD(user);
			userDB.Id = 0;
			dbUser.Add(userDB);
			dbUser.Save();
			return 0;
		}

		public int AddCompletedTask(bl.CompletedTask completedTask)
		{
			db.CompletedTask completedTaskDB = converter.ConvertCompletedTaskToBD(completedTask);

			// Проверка на то, что пользователь уже решил данную задачу.
			CompletedTask tmp =
						(from p in dbCompletedTask.GetCompletedTasks()
						 where p.UserId == completedTask.UserId && p.TaskId == completedTask.TaskId
						 select p).FirstOrDefault();

			if (tmp != null)
			{
				Console.WriteLine("User has already solved this task");
				return 0;
			}

			// Проверка на то, что существует пользователь и задача.
			bl.User user = GetUser(completedTask.UserId);
			if (user is null)
			{
				Console.WriteLine("User not found");
				return 0;
			}
			bl.Task task = GetTask(completedTask.TaskId);
			if (task is null)
			{
				Console.WriteLine("Task not found");
				return 0;
			}

			completedTaskDB.Id = 0;
			dbCompletedTask.Add(completedTaskDB);
			dbCompletedTask.Save();
			return 0;
		}

		public bl.CompletedTask GetCompletedTask(int id)
		{
			db.CompletedTask completedTask = dbCompletedTask.GetCompletedTask(id);
			return converter.ConvertCompletedTaskToBL(completedTask);
		}

		public bl.Task GetTask(int id)
		{
			db.Task task = dbTask.GetTask(id);
			if (task is null)
			{
				throw new Exception("Task not found");
			}
			return converter.ConvertTaskToBL(task);
		}

		public bl.User GetUser(int id)
		{
			db.User user = dbUser.GetUser(id);
			return converter.ConvertUserToBL(user);
		}

		public bl.User GetUserByEmail(string email)
		{
			db.User user = dbUser.GetUserByEmail(email);
			return converter.ConvertUserToBL(user);
		}
		public bl.User GetUserByLogin(string login)
		{
			db.User user = dbUser.GetUserByLogin(login);
			return converter.ConvertUserToBL(user);
		}
	}
}
