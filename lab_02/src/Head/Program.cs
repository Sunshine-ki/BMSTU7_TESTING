using System;
using System.Collections.Generic;

using bl;
using db;

namespace Head
{
	class Program
	{
		static void Main(string[] args)
		{
			Facade facade = new Facade();

			bl.Task newTask = new bl.Task(37, "Какое-то название задачи", "D", "DD", "S", "TN", 4);
			facade.AddTask(newTask);

			bl.User newUser = new bl.User(37, "Какое-то имя пользователя", "NewSurname", "NewEmail", "A", "TN", 4);
			facade.AddUser(newUser);

			bl.CompletedTask newCompletedTask = new bl.CompletedTask(13, 21, 14);
			facade.AddCompletedTask(newCompletedTask);

			foreach (bl.CompletedTask elem in facade.GetCompletedTasks())
				Console.WriteLine($"{elem.Id}  {elem.UserId} {elem.TaskId}");
			Console.WriteLine("\n");

			foreach (var elem in facade.GetUsers())
				Console.WriteLine($"{elem.Id} {elem.Name} {elem.Surname} {elem.Email} {elem.Login} {elem.Password} {elem.UserType}");
			Console.WriteLine("\n");

			foreach (var elem in facade.GetTasks())
				Console.WriteLine($"{elem.Id} {elem.Name} {elem.ShortDescription}");

			Console.WriteLine("\n");

			bl.CompletedTask completedTask = facade.GetCompletedTask(12);
			Console.WriteLine($"{completedTask.Id}  {completedTask.UserId} {completedTask.TaskId}");

			bl.Task task = facade.GetTask(25);
			Console.WriteLine($"{task.Id} {task.Name} {task.ShortDescription}");

			bl.User user = facade.GetUser(16);
			Console.WriteLine($"{user.Id} {user.Name} {user.Surname} {user.Email} {user.Login} {user.Password} {user.UserType}");



		}
	}
}
