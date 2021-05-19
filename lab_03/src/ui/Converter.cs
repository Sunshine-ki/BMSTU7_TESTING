
using System;
using System.Collections.Generic;

using bl;
using Head;

namespace ui
{
	public class Converter
	{
		// public ui.User ConvertUserToBD(bl.User user)
		// {
		// 	ui.User userDB = new db.User();
		// 	userDB.Id = user.Id;
		// 	userDB.Name = user.Name;
		// 	userDB.Surname = user.Surname;
		// 	userDB.Email = user.Email;
		// 	userDB.Login = user.Login;
		// 	userDB.Password = user.Password;
		// 	userDB.UserType = user.UserType;
		// 	// userDB.CreationTime = user.CreationTime;
		// 	return userDB;
		// }
		// public db.Task ConvertTaskToBD(bl.Task task)
		// {
		// 	db.Task taskDB = new db.Task();
		// 	taskDB.Id = task.Id;
		// 	taskDB.Name = task.Name;
		// 	taskDB.ShortDescription = task.ShortDescription;
		// 	taskDB.DetailedDescription = task.DetailedDescription;
		// 	taskDB.Solution = task.Solution;
		// 	taskDB.TableName = task.TableName;
		// 	taskDB.AuthorId = task.AuthorId;
		// 	return taskDB;
		// }
		// public db.CompletedTask ConvertCompletedTaskToBD(bl.CompletedTask completedTask)
		// {
		// 	db.CompletedTask completedTaskDB = new db.CompletedTask();
		// 	completedTaskDB.Id = completedTask.Id;
		// 	completedTaskDB.UserId = completedTask.UserId;
		// 	completedTaskDB.TaskId = completedTask.TaskId;
		// 	return completedTaskDB;
		// }

		public bl.User ConvertUserToBL(ui.Models.User user)
		{
			if (user is null)
				return null;
			return new bl.User(user.Id, user.Name, user.Surname, user.Email, user.Login, user.Password, user.UserType);
		}
		// public bl.Task ConvertTaskToBL(db.Task task)
		// {
		// 	if (task is null)
		// 		return null;
		// 	return new bl.Task(task.Id, task.Name, task.ShortDescription, task.DetailedDescription, task.Solution, task.TableName, task.AuthorId);
		// }

		// public bl.CompletedTask ConvertCompletedTaskToBL(db.CompletedTask completedTask)
		// {
		// 	if (completedTask is null)
		// 		return null;
		// 	return new bl.CompletedTask(completedTask.Id, completedTask.UserId, completedTask.TaskId);
		// }
	}
}
