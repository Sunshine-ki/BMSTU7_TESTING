using System;
using System.Collections.Generic;

namespace bl
{
	public interface IFacade
	{
		List<bl.Task> GetTasks();
		List<bl.User> GetUsers();
		List<bl.CompletedTask> GetCompletedTasks();
		// TODO: void ли ошибки
		int AddTask(bl.Task task);
		int AddUser(bl.User user);
		int AddCompletedTask(bl.CompletedTask completedTask);
		bl.CompletedTask GetCompletedTask(int id);
		bl.Task GetTask(int id);
		bl.User GetUser(int id);
		bl.User GetUserByEmail(string email);
		bl.User GetUserByLogin(string login);
	}
}
