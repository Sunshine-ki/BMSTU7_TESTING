using System;

namespace ui.Models
{
	public class User
	{
		public User()
		{
			Name = "";
			Surname = "";
			Email = "";
			Login = "";
			Password = "";
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public int UserType { get; set; }
	}
}
