using System;
using System.Collections.Generic;

namespace project
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