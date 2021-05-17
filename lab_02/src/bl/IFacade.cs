using System;
using System.Collections.Generic;

namespace bl
{
	public interface IFacade
	{
		List<bl.Task> GetTasks();
		List<bl.User> GetUsers();
		List<bl.CompletedTask> GetCompletedTasks();
		// void AddTask(bl.Task task); // TODO: void ли ? Ошибки 

	}
}
