using System;
using System.Collections.Generic;

namespace db
{
	public interface IRepositoryCompletedTask // : IDisposable
	{
		List<CompletedTask> GetCompletedTasks();
		CompletedTask GetCompletedTask(int id);
		List<CompletedTask> GetCompletedTaskByUserId(int id);
		List<CompletedTask> GetCompletedTaskByTaskId(int id);
		void Add(CompletedTask task);
		void Delete(int id);
		void Save();
	}
}