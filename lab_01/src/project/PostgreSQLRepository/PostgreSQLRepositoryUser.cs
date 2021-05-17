using System;
using System.Collections.Generic;
using System.Linq;

namespace db
{
	public class PostgreSQLRepositoryUser : IRepositoryUser
	{
		private ApplicationContext db;

		public PostgreSQLRepositoryUser()
		{
			this.db = new ApplicationContext();
		}
		public User GetUser(int id)
		{
			// var e = from p in db.Users
			// 		where p.Id == id
			// 		select p;
			// Find - Поиск по PK.
			return db.Users.Find(id);
		}

		public List<User> GetUsers()
		{
			return db.Users.ToList();
		}

		public void Add(User user)
		{
			db.Users.Add(user);
		}

		public void Save()
		{
			db.SaveChanges();
		}


		public void UpdateName(int id, string newName)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Name = newName;
			}
		}
		public void UpdateSurname(int id, string newSurname)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Surname = newSurname;
			}
		}
		public void UpdateEmail(int id, string newEmail)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Email = newEmail;
			}
		}
		public void UpdateLogin(int id, string newLogin)
		{
			User user = db.Users.Find(id);
			if (user != null)
			{
				user.Login = newLogin;
			}
		}

		public void Delete(int id)
		{
			User user = db.Users.Find(id);
			if (user != null)
				db.Users.Remove(user);
		}
	}
}