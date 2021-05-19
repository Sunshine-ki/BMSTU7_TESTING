using System;
using System.Collections.Generic;
using System.Linq;

using bl;
using db;

namespace Head
{
	public class Facade
	{
		IFacade conFacade;
		public Facade() => conFacade = new ConFacade();

		public List<bl.CompletedTask> GetCompletedTasks()
		{
			return conFacade.GetCompletedTasks();
		}

		public List<bl.User> GetUsers()
		{
			return conFacade.GetUsers();
		}

		public List<bl.Task> GetTask()
		{
			return conFacade.GetTasks();
		}

		public int AddCompletedTask(bl.CompletedTask completedTask)
		{
			conFacade.AddCompletedTask(completedTask);
			return 0;
		}

		Head.Answer CheckPassword(string password)
		{
			if (password.Length < 6)
				return new Head.Answer((int)Constants.Errors.ShortLengthPassword, "Длина пароля должна быть больше 5 символов");

			if (Extensions.IsNumeric(password))
				return new Head.Answer((int)Constants.Errors.OnlyNumericPassword, "Пароль не должен состоять только из цифр");

			return new Head.Answer(Constants.OK, "Ok");
		}

		public Head.Answer AddUser(bl.User user)
		{
			bl.User userOld = conFacade.GetUserByEmail(user.Email);
			if (userOld != null)
				return new Head.Answer((int)Constants.Errors.EmailUserExists, "Данный email занят");

			userOld = conFacade.GetUserByLogin(user.Login);
			if (userOld != null)
				return new Head.Answer((int)Constants.Errors.LoginUserExists, "Данный логин занят");


			Head.Answer checkPassword = CheckPassword(user.Password);
			if (checkPassword.returnValue != Constants.OK)
			{
				return checkPassword;
			}

			conFacade.AddUser(user);
			return new Head.Answer(Constants.OK, "Ok");
		}

		public Head.Answer AddTask(bl.Task task)
		{
			conFacade.AddTask(task);
			return new Head.Answer(Constants.OK, "Ok");
		}

		public bl.CompletedTask GetCompletedTask(int id)
		{
			return conFacade.GetCompletedTask(id);
		}

		public bl.Task GetTask(int id)
		{
			return conFacade.GetTask(id);
		}

		public bl.User GetUser(int id)
		{
			return conFacade.GetUser(id);
		}

	}
}
