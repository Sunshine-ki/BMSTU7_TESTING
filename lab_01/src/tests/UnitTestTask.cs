using System;
using System.Linq;
using Xunit;
using db;

namespace tests
{
	public class UnitTestTask
	{
		[Fact]
		public void TestAdd()
		{
			IRepositoryTask db = new PostgreSQLRepositoryTask();
			Task newTask = new Task()
			{
				Name = "The first",
				ShortDescription = "ShortDescription",
				DetailedDescription = "DetailedDescription",
				Solution = "SELECT 5!",
				TableName = "VR",
				AuthorId = 4
			};

			db.Add(newTask);
			db.Save();

			int id = db.GetTasks().Max(p => p.Id);
			var task = (from u in db.GetTasks()
						where u.Id == id
						select u).ToList()[0];

			Assert.Equal(newTask.Name, task.Name);
			Assert.Equal(newTask.ShortDescription, task.ShortDescription);
			Assert.Equal(newTask.DetailedDescription, task.DetailedDescription);
			Assert.Equal(newTask.Solution, task.Solution);
			Assert.Equal(newTask.TableName, task.TableName);
			Assert.Equal(newTask.AuthorId, task.AuthorId);
		}

		[Fact]
		public void TestDelete()
		{
			// foreach (var elem in db.GetTasks())
			// Console.WriteLine($"{elem.Id} {elem.Name}");

			const int id = 14;

			IRepositoryTask db = new PostgreSQLRepositoryTask();
			db.Delete(id);

			if (db.GetTask(id) is null)
				throw new Exception("Problem in delete task");

		}

		[Fact]
		public void TestDeleteSave()
		{
			const int id = 15;

			IRepositoryTask db = new PostgreSQLRepositoryTask();
			db.Delete(id);
			db.Save();

			if (db.GetTask(id) != null)
				throw new Exception("Problem in delete task");

			// Assert.IsNull(null);
			// Assert.False(false, "error text");
		}

	}
}