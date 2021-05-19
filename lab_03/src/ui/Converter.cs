
using System;
using System.Collections.Generic;

using bl;
using Head;

namespace ui
{
	public class Converter
	{
		public ui.Models.User ConvertUserToUI(bl.User user)
		{
			ui.Models.User userUI = new ui.Models.User();
			userUI.Id = user.Id;
			userUI.Name = user.Name;
			userUI.Surname = user.Surname;
			userUI.Email = user.Email;
			userUI.Login = user.Login;
			userUI.Password = user.Password;
			userUI.UserType = user.UserType;
			// userUI.CreationTime = user.CreationTime;
			return userUI;
		}
		public ui.Models.Task ConvertTaskToUI(bl.Task task)
		{
			ui.Models.Task taskUI = new ui.Models.Task();
			taskUI.Id = task.Id;
			taskUI.Name = task.Name;
			taskUI.ShortDescription = task.ShortDescription;
			taskUI.DetailedDescription = task.DetailedDescription;
			taskUI.Solution = task.Solution;
			taskUI.TableName = task.TableName;
			taskUI.AuthorId = task.AuthorId;
			return taskUI;
		}
		// public db.CompletedTask ConvertCompletedTaskToBD(bl.CompletedTask completedTask)
		// {
		// 	db.CompletedTask completedtaskUI = new db.CompletedTask();
		// 	completedtaskUI.Id = completedTask.Id;
		// 	completedtaskUI.UserId = completedTask.UserId;
		// 	completedtaskUI.TaskId = completedTask.TaskId;
		// 	return completedtaskUI;
		// }

		public bl.User ConvertUserToBL(ui.Models.User user)
		{
			if (user is null)
				return null;
			return new bl.User(user.Id, user.Name, user.Surname, user.Email, user.Login, user.Password, user.UserType);
		}

		public bl.Task ConvertTaskToBL(ui.Models.Task task)
		{
			if (task is null)
				return null;
			return new bl.Task(task.Id, task.Name, task.ShortDescription, task.DetailedDescription, task.Solution, task.TableName, task.AuthorId);
		}

	}
}
