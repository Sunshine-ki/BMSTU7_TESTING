using System;
using System.Collections.Generic;

namespace db
{
	public interface IRepositoryTask : IDisposable
	{
		List<Task> GetTasks();
		Task GetTask(int id);
		void Add(Task task);
		void Delete(int id);
		void Save();
	}
}