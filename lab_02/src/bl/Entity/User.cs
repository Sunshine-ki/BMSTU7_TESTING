using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel;

namespace bl
{
	public class User
	{
		public User(int id, string name, string surname, string email, string login, string password, int userType)
		{
			Id = id;
			Name = name;
			Surname = surname;
			Email = email;
			Login = login;
			Password = password;
			UserType = userType;
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int UserType { get; set; }
		// public DateTime CreationTime { get; set; } = DateTime.Now;
		// public List<Task> Tasks { get; set; }
		// public List<CompletedTask> CompletedTasks { get; set; }
	}
}