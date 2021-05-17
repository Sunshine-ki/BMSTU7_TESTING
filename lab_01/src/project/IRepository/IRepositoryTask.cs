using System;
using System.Collections.Generic;

namespace db
{
	// TODO: Сделать шаблонный IRepository?
	public interface IRepositoryTask : IDisposable
	{
		List<Task> GetTasks();
		Task GetTask(int id);
		void Add(Task task);
		void Delete(int id);
		void Save();
	}
}